using BoxProtocol;
using MagicOnion;
using MagicOnion.Server;

namespace BoxServerCore
{
    public class HwDebugBoxUiService : ServiceBase<IDebugUIHardwareBoxService>, IDebugUIHardwareBoxService
    {
        public UnaryResult<bool> Sell(string Where, int What)
        {   
            return new UnaryResult<bool>(HwDebugRegestry.purchases.TryAdd(Where, What));
        }
    }
}