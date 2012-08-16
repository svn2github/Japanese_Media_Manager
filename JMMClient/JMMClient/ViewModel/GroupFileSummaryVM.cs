﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMMClient.ViewModel
{
	public class GroupFileSummaryVM
	{
		public string GroupName { get; set; }
		public string GroupNameShort { get; set; }
		public int FileCountNormal { get; set; }
		public bool NormalComplete { get; set; }
		public int FileCountSpecials { get; set; }
		public bool SpecialsComplete { get; set; }

		public List<int> NormalEpisodeNumbers { get; set; }
		public string NormalEpisodeNumberSummary { get; set; }

		public bool HasAnySpecials
		{
			get
			{
				return FileCountSpecials > 0;
			}
		}

		public GroupFileSummaryVM(JMMServerBinary.Contract_GroupFileSummary contract)
		{
			this.GroupName = contract.GroupName;
			this.GroupNameShort = contract.GroupNameShort;
			this.FileCountNormal = contract.FileCountNormal;
			this.FileCountSpecials = contract.FileCountSpecials;
			this.NormalComplete = contract.NormalComplete;
			this.SpecialsComplete = contract.SpecialsComplete;

			this.NormalEpisodeNumbers = contract.NormalEpisodeNumbers;
			this.NormalEpisodeNumberSummary = contract.NormalEpisodeNumberSummary;
		}

		public string PrettyDescription
		{
			get
			{
				return this.ToString();
			}
		}

		public override string ToString()
		{
			return string.Format("{0} - {1}/{2} Files", GroupNameShort, FileCountNormal, FileCountSpecials);
		}
	}
}
