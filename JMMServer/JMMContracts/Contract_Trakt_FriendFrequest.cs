﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_Trakt_FriendFrequest
	{
		public string Username { get; set; }
		public string Full_name { get; set; }
		public string Gender { get; set; }
		public object Age { get; set; }
		public string Location { get; set; }
		public string About { get; set; }
		public int Joined { get; set; }
		public int Requested { get; set; }
		public DateTime? RequestedDate { get; set; }
		public DateTime? JoinedDate { get; set; }
		public string Avatar { get; set; }
		public string Url { get; set; }
	}
}
