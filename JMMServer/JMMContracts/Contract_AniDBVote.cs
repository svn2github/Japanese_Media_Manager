﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMContracts
{
	public class Contract_AniDBVote
	{
		public int EntityID { get; set; }
		public int VoteType { get; set; }
		public decimal VoteValue { get; set; } // out of 10

		public Contract_AniDBVote()
		{
		}
	}
}
