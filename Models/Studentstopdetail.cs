
using System.ComponentModel.DataAnnotations.Schema;

namespace IvisMaui.Models
{
    public class Studentstopdetail
    {
		public int Id { get; set; }
        public string BusNumber { get; set; } //todo
        public string RouteId { get; set; } //todo
        public string StopId { get; set; }
        public string AmPm { get; set; }  //todo
        public string StudentNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }
		public string Grade { get; set; }
		public string DOB { get; set; }
		public string School { get; set; }
		public string SeatNumber { get; set; }
		public int Status { get; set; }
		public string Arrival { get; set; }
		public string Intersection1 { get; set; }
		public string Intersection2 { get; set; }
        [NotMapped]
        public string Intersections
        {
            get
            {
                return Intersection1 ?? "" + " & " + Intersection2 ?? "";
            }
        }

        [NotMapped]
        public string Image
        {
            get
            {
                if (String.IsNullOrEmpty(StudentNumber))
                {
                    return "notfound.jpg";
                }
                else
                {
                    return "a" + StudentNumber + "a.jpg";
                }
            }
        }


        [NotMapped]
        public string StatusDisplay
        {
            get
            {
                if (Status == 1)
                {
                    return "On";
                }
                else
                {
                    return "Off";
                }
            }
        }
            //public string FullName
            //{
            //	get { return $"{LastName}, {FirstName}"; }
            //}

    }

    //public class StudentStopRow
    //{
    //	public string FullName { get; set; }
    //	public string Grade { get; set; }
    //	public string SeatNumber { get; set; }
    //	//public DataGridViewButtonColumn Status { get; set; }
    //       public string Status { get; set; }
    //       public string StudentNumber { get; set; }
    //	public string Phone { get; set; }
    //	public string DOB { get; set; }
    //}

}
