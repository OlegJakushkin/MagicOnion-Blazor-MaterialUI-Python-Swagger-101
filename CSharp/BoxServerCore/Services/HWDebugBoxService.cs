using System;
using System.Collections.Generic;
using BoxProtocol;
using MagicOnion;
using MagicOnion.Server;

namespace BoxServerCore
{
    public class HWDebugBoxService : ServiceBase<IHardwareBoxService>, IHardwareBoxService
    {
        public UnaryResult<BoxTask> UpdateStatus(StatusUpdateRequest code)
        {
            var tosell = 0;
            var doit = HwDebugRegestry.purchases.TryGetValue(code.HardwareId, out tosell);
            if (doit)
            {
                return UnaryResult(BoxTask.StartServeing);
            }
            else
            {
                return UnaryResult(BoxTask.StandBy);
            }
        }

        public UnaryResult<ServeRequest> BeginServeRequest(string HardwareId)
        {
            var tosell = 0;
            var doit = HwDebugRegestry.purchases.TryGetValue(HardwareId, out tosell);
            var rId = Guid
                .NewGuid()
                .ToString();
            if (doit)
            {
                HwDebugRegestry.executingRequests.Add(rId, new KeyValuePair<string, int>(HardwareId, tosell));
                HwDebugRegestry.purchases.Remove(HardwareId);
                return UnaryResult(new ServeRequest()
                {
                    HardwareId = HardwareId,
                    IsCanceled = false,
                    RequestId = rId,
                    Position = tosell
                });
            }
            else
            {
                return UnaryResult(new ServeRequest()
                {
                    HardwareId = HardwareId,
                    IsCanceled = true
                });
            }
        }

        public UnaryResult<BoxTask> EndServeRequest(ServeResponse code)
        {
            HwDebugRegestry.executingRequests.Remove(code.RequestId);
            switch (code.Code)
            {
                case StatusCode.Error:
                {
                    return new UnaryResult<BoxTask>(BoxTask.StandBy);
                }
                case StatusCode.Ok:
                {
                    return new UnaryResult<BoxTask>(BoxTask.StandBy);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public UnaryResult<string> InitHardware()
        {
            var newid = Guid
                .NewGuid()
                .ToString();
            HwDebugRegestry.HwIDs.Add(newid);
            return new UnaryResult<string>(newid);
        }
    }
}