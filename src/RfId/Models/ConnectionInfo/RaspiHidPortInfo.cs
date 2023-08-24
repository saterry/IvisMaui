namespace RfId.Core.Models.ConnectionInfo
{
    public class RaspiHidPortInfo : BaseRfIdInfo
    {
        public string HidPort { get; set; } = "ComPort1";

        public RaspiHidPortInfo()
        {
            ReadFrequenty = 1000;
        }

        public RaspiHidPortInfo(string hidPort, int readFrequenty = 1000)
        {
            HidPort = hidPort;
            ReadFrequenty = readFrequenty;
        }
    }
}