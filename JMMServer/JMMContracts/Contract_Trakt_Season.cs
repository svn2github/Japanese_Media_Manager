﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_Trakt_Season
	{
		public int Trakt_SeasonID { get; set; }
		public int Trakt_ShowID { get; set; }
		public int Season { get; set; }
		public string URL { get; set; }
		public List<Contract_Trakt_Episode> Episodes { get; set; }
	}
}
