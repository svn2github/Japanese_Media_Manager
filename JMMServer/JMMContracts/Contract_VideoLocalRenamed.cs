﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_VideoLocalRenamed
	{
		public int VideoLocalID { get; set; }
		public Contract_VideoLocal VideoLocal { get; set; }
		public string NewFileName { get; set; }
		public bool Success { get; set; }
	}
}
