﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyAnimePlugin3.Downloads
{
	public class TorrentLink
	{
		public string Source { get; set; }
		public string SourceLong { get; set; }

		private string torrentName;
		public string TorrentName
		{
			get 
			{

				string newName = "";
				foreach (char chr in torrentName.ToCharArray())
				{
					if ((int)chr >= 34 && (int)chr <= 40) continue;
					if ((int)chr >= 58 && (int)chr <= 64) continue;

					if ((int)chr <= 128)
						newName += chr.ToString();
				}


				return newName; 
			}
			set { torrentName = value; }
		}

		private string torrentDownloadLink;
		public string TorrentDownloadLink
		{
			get
			{
				if (Source == "BakaBT")
				{
					if (string.IsNullOrEmpty(torrentDownloadLink))
					{
						TorrentsBakaBT bakbt = new TorrentsBakaBT();
						torrentDownloadLink = bakbt.PopulateTorrentLink(TorrentLinkURL);
					}
				}
				return torrentDownloadLink;
			}
			set { torrentDownloadLink = value; }
		}

		public string Size { get; set; }
		public string Seeders { get; set; }
		public string Leechers { get; set; }
		public string TorrentLinkURL { get; set; }

		public override string ToString()
		{
			return string.Format("Torrent:   {0} - {1}", TorrentName, TorrentDownloadLink);
		}
	}
}
