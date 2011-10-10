using System;
using System.Collections.Generic;
using System.Text;


namespace AniDBAPI
{
	public class Raw_AniDB_GroupStatus : XMLBase
	{
        public int AnimeID  { get; set; }
		public int GroupID { get; set; }
        public string GroupName { get; set; }
		public int CompletionState { get; set; }
		public int LastEpisodeNumber { get; set; }
		public int Rating { get; set; }
		public int Votes { get; set; }
		public string EpisodeRange { get; set; }

		private void PopulateDefaults()
		{
			AnimeID = 0;
			GroupID = 0;
			GroupName = string.Empty;
			CompletionState = 0;
			LastEpisodeNumber = 0;
			Rating = 0;
			Votes = 0;
			EpisodeRange = string.Empty;
		}

		public Raw_AniDB_GroupStatus()
		{
			PopulateDefaults();
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("AniDB_GroupStatus:: AnimeID: " + AnimeID.ToString());
			sb.Append(" | GroupName: " + GroupName);
			sb.Append(" | LastEpisode: " + LastEpisodeNumber);

			return sb.ToString();
		}

	}
}
