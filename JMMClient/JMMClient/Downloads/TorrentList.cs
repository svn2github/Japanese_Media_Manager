﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMClient.Downloads
{
	public class TorrentList
	{
		public int build { get; set; }
		public List<object> label { get; set; }
		public List<object[]> torrents { get; set; }
		public int torrentc { get; set; }

		public List<Torrent> TorrentObjects { get; set; }

	}
}
