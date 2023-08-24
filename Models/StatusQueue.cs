
namespace IvisMaui.Models
{
    public class StatusQueue
    {
        //public StatusQueue(string BusNumber, string StopId, string StudentNumber, int StatusId, string StatusDate)
        //{
        //    this.BusNumber = BusNumber;
        //    this.StopId = StopId;
        //    this.StudentNumber = StudentNumber;
        //    this.Status = Status;
        //    this.StatusDate = StatusDate;
        //}

        public int DistrictId { get; set; }
        public string BusNumber { get; set; }
        public string RouteId { get; set; }
        public string AmPm { get; set; }
        public string StopId { get; set; }
        public string StudentNumber { get; set; }
        public int Status { get; set; }
        public string StatusDate { get; set; }
        public int Uploaded { get; set; }

    }
}
