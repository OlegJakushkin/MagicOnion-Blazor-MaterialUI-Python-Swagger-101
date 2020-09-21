using MessagePack;

namespace BoxProtocol
{
    [MessagePackObject]
    public class StatusUpdateRequest
    {
        [Key(0)]
        public StatusCode Code { get; set; }

        [Key(1)]
        public string HardwareId { get; set; }
    }
}