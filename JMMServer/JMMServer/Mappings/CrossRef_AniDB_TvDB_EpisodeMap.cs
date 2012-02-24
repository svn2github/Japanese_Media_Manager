﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Entities;
using FluentNHibernate.Mapping;

namespace JMMServer.Mappings
{
	public class CrossRef_AniDB_TvDB_EpisodeMap : ClassMap<CrossRef_AniDB_TvDB_Episode>
	{
		public CrossRef_AniDB_TvDB_EpisodeMap()
        {
			Not.LazyLoad();
			Id(x => x.CrossRef_AniDB_TvDB_EpisodeID);

			Map(x => x.AnimeID).Not.Nullable();
			Map(x => x.AniDBEpisodeID).Not.Nullable();
			Map(x => x.TvDBEpisodeID).Not.Nullable();
        }
	}
}
