﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class AniDB_Anime_SimilarMap : ClassMap<AniDB_Anime_Similar>
	{
		public AniDB_Anime_SimilarMap()
        {
			Not.LazyLoad();
            Id(x => x.AniDB_Anime_SimilarID);

			Map(x => x.AnimeID).Not.Nullable();
			Map(x => x.Approval).Not.Nullable();
			Map(x => x.SimilarAnimeID).Not.Nullable();
			Map(x => x.Total).Not.Nullable();
        }
	}
}
