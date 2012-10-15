﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_VideoLocalManualLink
	{
		public int VideoLocalID { get; set; }
		public string FilePath { get; set; }
		public int ImportFolderID { get; set; }
		public string Hash { get; set; }
		public string CRC32 { get; set; }
		public string MD5 { get; set; }
		public string SHA1 { get; set; }
		public int HashSource { get; set; }
		public long FileSize { get; set; }
		public int IsWatched { get; set; }
		public int IsIgnored { get; set; }
		public DateTime? WatchedDate { get; set; }
		public DateTime DateTimeUpdated { get; set; }
		public int IsVariation { get; set; }

		public Contract_ImportFolder ImportFolder { get; set; }


	}
}
