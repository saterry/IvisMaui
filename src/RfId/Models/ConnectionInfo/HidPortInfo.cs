namespace RfId.Core.Models.ConnectionInfo
{
    public class HidPortInfo : BaseRfIdInfo
    {
        public string HidPort { get; set; } = "ComPort1";

        public HidPortInfo()
        {
            ReadFrequenty = 1000;
        }

        public HidPortInfo(string hidPort, int readFrequenty = 1000)
        {
            HidPort = hidPort;
            ReadFrequenty = readFrequenty;
        }
    }
}
