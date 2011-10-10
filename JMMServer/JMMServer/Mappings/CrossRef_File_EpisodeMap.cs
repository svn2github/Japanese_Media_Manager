﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class CrossRef_File_EpisodeMap : ClassMap<CrossRef_File_Episode>
	{
		public CrossRef_File_EpisodeMap()
        {
			Not.LazyLoad();
            Id(x => x.CrossRef_File_EpisodeID);

			Map(x => x.CrossRefSource).Not.Nullable();
			Map(x => x.EpisodeID).Not.Nullable();
			Map(x => x.EpisodeOrder).Not.Nullable();
			Map(x => x.Hash).Not.Nullable();
			Map(x => x.Percentage).Not.Nullable();
			Map(x => x.FileName).Not.Nullable();
			Map(x => x.FileSize).Not.Nullable();
			Map(x => x.AnimeID).Not.Nullable();
        }
	}
}
