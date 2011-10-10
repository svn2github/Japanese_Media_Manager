using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NLog;

namespace AniDBAPI
{
	public class Raw_AniDB_MyListFile
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public int ListID { get; set; }
		public int FileID { get; set; }
		public int EpisodeID { get; set; }
		public int AnimeID { get; set; }
		public int GroupID { get; set; }
		public int State { get; set; }
		public string FileDate { get; set; }
		public DateTime? WatchedDate { get; set; }
		public int ViewDateUDP { get; set; }
		public string ViewDateHTTP { get; set; }
		public string Storage { get; set; }
		public string Source { get; set; }
		public string Other { get; set; }
		public int FileState { get; set; }

		public bool IsWatched
		{
			get
			{
				if (this.ViewDateUDP > 0 || this.ViewDateHTTP.Trim().Length > 0)
					return true;
				else
					return false;
			}
		}

		// default constructor
		public Raw_AniDB_MyListFile()
		{
		}

		private void InitVals()
		{
			ListID = 0;
			FileID = 0;
			EpisodeID = 0;
			AnimeID = 0;
			GroupID = 0;
			FileDate = "";
			ViewDateUDP = 0;
			ViewDateHTTP = "";
			Storage = "";
			Source = "";
			Other = "";
			FileState = 0;
			WatchedDate = null;
		}

		// constructor
		// sRecMessage is the message received from ANIDB file info command
		public Raw_AniDB_MyListFile(string sRecMessage)
		{
			// remove the header info
			string[] sDetails = sRecMessage.Substring(11).Split('|');

			// 221 MYLIST

			//{int4 lid}|{int4 fid}|{int4 eid}|{int4 aid}|{int4 gid}|{int4 date}|{int2 state}|{int4 viewdate}|{str storage}|{str source}|{str other}|{int2 filestate} 
			//  0. 66031082  ** list id
			//  1. 572794  ** file id
			//  2. 99294 ** episode id
			//  3. 6107 ** anime id
			//  4. 2723 ** group id
			//  5. 1239598714 ** fileDate
			//  6. 1 ** state
			//  7. 0  ** view date (will contain 0 if not watched, otherwise has the date it was watched)
			//  8.    ** storage
			//  9.    ** source
			// 10.    ** other
			// 11. 0  ** filestate

			this.ListID = AniDBAPILib.ProcessAniDBInt(sDetails[0]);
			this.FileID = AniDBAPILib.ProcessAniDBInt(sDetails[1]);
			this.EpisodeID = AniDBAPILib.ProcessAniDBInt(sDetails[2]);
			this.AnimeID = AniDBAPILib.ProcessAniDBInt(sDetails[3]);
			this.GroupID = AniDBAPILib.ProcessAniDBInt(sDetails[4]);
			this.FileDate = AniDBAPILib.ProcessAniDBString(sDetails[5]);
			this.State = AniDBAPILib.ProcessAniDBInt(sDetails[6]);
			this.ViewDateUDP = AniDBAPILib.ProcessAniDBInt(sDetails[7]);
			this.Storage = AniDBAPILib.ProcessAniDBString(sDetails[8]);
			this.Source = AniDBAPILib.ProcessAniDBString(sDetails[9]);
			this.Other = AniDBAPILib.ProcessAniDBString(sDetails[10]);
			this.FileState = AniDBAPILib.ProcessAniDBInt(sDetails[11]);

			// calculate the watched date
			if (ViewDateUDP > 0)
			{
				DateTime utcDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				utcDate = utcDate.AddSeconds(ViewDateUDP);

				WatchedDate = utcDate.ToLocalTime();
			}
		}

		public void ProcessHTTPSource(XmlNode node)
		{

			this.ListID = int.Parse(node.Attributes["id"].Value);
			this.AnimeID = int.Parse(node.Attributes["aid"].Value);
			this.EpisodeID = int.Parse(node.Attributes["eid"].Value);
			this.FileID = int.Parse(node.Attributes["fid"].Value);

			this.ViewDateHTTP = AniDBHTTPHelper.TryGetAttribute(node, "viewdate");

			// calculate the watched date
			if (!string.IsNullOrEmpty(ViewDateHTTP) && ViewDateHTTP.Length > 17)
			{
				try
				{
					// eg "2011-02-23T20:49:18+0000"
					int year = int.Parse(ViewDateHTTP.Trim().Substring(0, 4));
					int month = int.Parse(ViewDateHTTP.Trim().Substring(5, 2));
					int day = int.Parse(ViewDateHTTP.Trim().Substring(8, 2));

					int hour = int.Parse(ViewDateHTTP.Trim().Substring(11, 2));
					int minute = int.Parse(ViewDateHTTP.Trim().Substring(14, 2));
					int second = int.Parse(ViewDateHTTP.Trim().Substring(17, 2));

					DateTime utcDate = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
					utcDate = utcDate.AddSeconds(ViewDateUDP);

					WatchedDate = utcDate.ToLocalTime();
				}
				catch (Exception ex)
				{
					logger.ErrorException("Error processing View Date HTTP: " + ex.ToString(), ex);
				}
			}

			string tempstate = AniDBHTTPHelper.TryGetProperty(node, "state");
			int istate = 0;
			int.TryParse(tempstate, out istate);
			this.State = istate;

			string fstate = AniDBHTTPHelper.TryGetProperty(node, "filestate");
			int ifilestate = 0;
			int.TryParse(fstate, out ifilestate);
			this.FileState = ifilestate;

			this.Source = AniDBHTTPHelper.TryGetProperty(node, "storage");

		}

		public override string ToString()
		{
			return string.Format("Raw_AniDB_MyListFile:: fileID: {0} | IsWatched: {1}", FileID, IsWatched);
		}

	}
}
