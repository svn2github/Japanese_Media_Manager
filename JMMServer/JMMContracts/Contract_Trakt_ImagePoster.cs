﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_Trakt_ImagePoster
	{
		public int Trakt_ImagePosterID { get; set; }
		public int Trakt_ShowID { get; set; }
		public int Season { get; set; }
		public string ImageURL { get; set; }
		public int Enabled { get; set; }
	}
}
