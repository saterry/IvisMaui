namespace IvisMaui.Models
{
    public class Busmessage
    {
		public int Id { get; set; }
		public int BusId { get; set; }
		public string Message { get; set; }
		public string Sender { get; set; }
		public int Priority { get; set; }
		public string CreateDateTime { get; set; }
		public int Status { get; set; }
	}
}
