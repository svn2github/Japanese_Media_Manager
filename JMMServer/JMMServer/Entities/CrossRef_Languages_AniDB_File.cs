﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMServer.Entities
{
	public class CrossRef_Languages_AniDB_File
	{
		public int CrossRef_Languages_AniDB_FileID { get; private set; }
		public int FileID { get; set; }
		public int LanguageID { get; set; }
	}
}
