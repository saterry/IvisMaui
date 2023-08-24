using System.ComponentModel.DataAnnotations.Schema;

namespace IvisMaui.Models
{

    //Add Latitude and Longitude
    //Calculated
    //A2: x
    //B2: y
    //C2: =LEFT(B2,2) + (B2-(LEFT(B2,2)*100))/60
    //D2: =LEFT(A2,3) + (A2-(LEFT(A2,3)*100))/60


    public class Busstop
    {
		private string _x = "";
		private string _y = "";

		public int Id { get; set; }
		public int RouteId { get; set; }
		public int BusId { get; set; }
		public string AmPm { get; set; }
		//public int Depot { get; set; }
		public string StopId { get; set; }
		public string Arrival { get; set; }
		public string Intersection1 { get; set; }
		public string Intersection2 { get; set; }
        [NotMapped]
		public string Intersections
		{
			get
			{
				return Intersection1 + " & " + Intersection2;
            }
        }
        public string X
		{
			get => _x;
			set
			{
					_x = value;
			}
		}
		public string Y
		{
			get => _y;
			set
			{
				_y = value;
			}
		}

		//public string Latitude => (Int32.Parse(Y.Substring(0, 2)) + (Int32.Parse(Y) - (Int32.Parse(Y.Substring(0, 2)) * 100)) / 60).ToString();
		//public string Longitude => (Int32.Parse(X.Substring(0, 3)) + (Int32.Parse(X) - (Int32.Parse(X.Substring(0, 3)) * 100)) / 60).ToString();

		public string Latitude { get; set; }
		public string Longitude { get; set; }

		//public string Latitude
		//{
		//	get
		//	{
		//		if (_y == "")
		//		{
		//			return "0";
		//		}
		//		else
		//		{
		//			return (decimal.Parse(_y.Substring(0, 2)) + (decimal.Parse(_y) - (decimal.Parse(_y.Substring(0, 2)) * 100)) / 60).ToString();
		//		}
		//	}

		//	//get => (decimal.Parse(_y.Substring(0, 2)) + (decimal.Parse(_y) - (decimal.Parse(_y.Substring(0, 2)) * 100)) / 60).ToString();
		//}

		//public string Longitude
		//{
		//	get
		//	{
		//		if (_x == "")
		//		{
		//			return "0";
		//		}
		//		else
		//		{
		//			return (decimal.Parse(_x.Substring(0, 3)) + (decimal.Parse(_x) - (decimal.Parse(_x.Substring(0, 3)) * 100)) / 60).ToString();
		//		}
		//	}
		//	//get => (decimal.Parse(_x.Substring(0, 3)) + (decimal.Parse(_x) - (decimal.Parse(_x.Substring(0, 3)) * 100)) / 60).ToString();
		//}
	}

	public class BusStopRow
	{
		public string Arrival { get; set; }
		public string Intersection1 { get; set; }
		public string Intersection2 { get; set; }
		//public DataGridViewButtonColumn Status { get; set; }
        public string Status { get; set; }
    }


}
