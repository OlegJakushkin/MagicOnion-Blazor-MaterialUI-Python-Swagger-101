using MessagePack;

namespace BoxProtocol
{
    [MessagePackObject]
    public class ServeRequest
    {
        [Key(0)]
        public int Position { get; set; }

        [Key(1)]
        public string RequestId { get; set; }

        [Key(2)]
        public string HardwareId { get; set; }

        [Key(3)] 
        public bool IsCanceled { get; set; }

    }
}