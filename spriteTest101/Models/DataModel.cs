using System;
using SQLite;

namespace spriteTest101
{
	public class DataModel
	{

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Level1 { get; set; }
		public string Level2 { get; set; }
		public string Level3 { get; set; }
		public string Level4 { get; set; }
		public string Level5 { get; set; }
		public Byte[] Image1 { get; set; }
		public Byte[] Image2 { get; set; }
		public Byte[] Image3 { get; set; }


		public DataModel ()
		{


		}


	}
}

