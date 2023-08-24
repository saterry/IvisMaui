using System;
using RfId.Core.Enums;

namespace RfId.Core.Models.Events
{
    public class RfIdStatusEventArgs : EventArgs
    {
        public RfIdStatus Status { get; set; }

        public RfIdStatusEventArgs()
        {
            
        }

        public RfIdStatusEventArgs(RfIdStatus status)
        {
            Status = status;
        }
    }
}
