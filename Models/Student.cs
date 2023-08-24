
using System.ComponentModel.DataAnnotations.Schema;

namespace IvisMaui.Models
{
    public class Student
    {
		public int Id { get; set; }
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
		public string CardNumber { get; set; }
        public string Intersection1 { get; set; }
        public string Intersection2 { get; set; }
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

    }
}
