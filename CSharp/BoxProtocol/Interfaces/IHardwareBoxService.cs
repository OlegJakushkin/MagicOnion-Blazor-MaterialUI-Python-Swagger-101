using MagicOnion;

namespace BoxProtocol
{
    public interface IHardwareBoxService : IService<IHardwareBoxService>
    {
        //Send Current Satus
        UnaryResult<BoxTask> UpdateStatus(StatusUpdateRequest code);

        //Get Latest Serve Request
        UnaryResult<ServeRequest> BeginServeRequest(string HardwareId);

        //Report on Serve Request completion, get new task
        UnaryResult<BoxTask> EndServeRequest(ServeResponse code);

        //Get HardwareId
        UnaryResult<string> InitHardware();
    }
}