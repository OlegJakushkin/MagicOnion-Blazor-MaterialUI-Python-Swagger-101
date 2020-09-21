using MessagePack;

namespace BoxProtocol
{
    [MessagePackObject]
    public class ServeResponse
    {
        [Key(2)]
        public string HardwareId;

        [Key(0)]
        public StatusCode Code { get; set; }

        [Key(1)]
        public string RequestId { get; set; }
    }
}