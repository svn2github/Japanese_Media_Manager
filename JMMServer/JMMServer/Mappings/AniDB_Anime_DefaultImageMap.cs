﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class AniDB_Anime_DefaultImageMap : ClassMap<AniDB_Anime_DefaultImage>
	{
		public AniDB_Anime_DefaultImageMap()
        {
			Not.LazyLoad();
            Id(x => x.AniDB_Anime_DefaultImageID);

			Map(x => x.AnimeID).Not.Nullable();
			Map(x => x.ImageParentID).Not.Nullable();
			Map(x => x.ImageParentType).Not.Nullable();
			Map(x => x.ImageType).Not.Nullable();
        }
	}
}
