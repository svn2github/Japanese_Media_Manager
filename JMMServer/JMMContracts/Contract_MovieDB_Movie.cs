﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_MovieDB_Movie
	{
		public int MovieDB_MovieID { get; set; }
		public int MovieId { get; set; }
		public string MovieName { get; set; }
		public string OriginalName { get; set; }
		public string Overview { get; set; }
	}
}
