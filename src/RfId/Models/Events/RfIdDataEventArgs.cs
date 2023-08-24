using System;

namespace RfId.Models.Events
{
    public class RfIdDataEventArgs : EventArgs
    {

        public string CardNumber { get; set; }
        public string StudentNumber { get; set; }

        public RfIdDataEventArgs(string CardNumber)
        {
            this.CardNumber = CardNumber;
            //this.StudentNumber = StudentNumber;
        }

        public override string ToString()
        {
            return $"CardNumber: {CardNumber}";
        }

    }
}
