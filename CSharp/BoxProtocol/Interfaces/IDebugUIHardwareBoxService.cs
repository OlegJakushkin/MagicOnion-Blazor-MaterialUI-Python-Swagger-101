using MagicOnion;

namespace BoxProtocol
{
    public interface IDebugUIHardwareBoxService : IService<IDebugUIHardwareBoxService>
    {
        UnaryResult<bool> Sell(string Where, int What);
    }
}