namespace RfId.Core.Models.ConnectionInfo
{
    public class RaspiPortInfo : BaseRfIdInfo
    {
        public string ComPort { get; set; } = "ComPort1";

        public RaspiPortInfo()
        {
            ReadFrequenty = 1000;
        }

        public RaspiPortInfo(string comPort, int readFrequenty = 1000)
        {
            ComPort = comPort;
            ReadFrequenty = readFrequenty;
        }
    }
}