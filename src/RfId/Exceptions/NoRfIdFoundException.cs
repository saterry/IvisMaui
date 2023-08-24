using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RfId.Core.Exceptions
{
    public class NoRfIdFoundException : Exception
    {
        public NoRfIdFoundException() : base("There is no rfid reader found in this device!")
        {
            
        }
    }
}
