namespace IvisMaui.Models
{
    public class Bus
    {
		public int Id { get; set; }
		public string BusId { get; set; }
		public string Number { get; set; }
		public int Compound { get; set; }
		public string Driver { get; set; }
		public string Monitor { get; set; }
		public string Other { get; set; }
	}

	public class BusSelect
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
