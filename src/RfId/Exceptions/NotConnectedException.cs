using System;

namespace RfId.Core.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException() : base("The connection is not open. Plz connect first!")
        {

        }
    }
}
