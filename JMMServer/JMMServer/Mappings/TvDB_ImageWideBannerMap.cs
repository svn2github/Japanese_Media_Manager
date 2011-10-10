﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class TvDB_ImageWideBannerMap : ClassMap<TvDB_ImageWideBanner>
	{
		public TvDB_ImageWideBannerMap()
        {
			Not.LazyLoad();
            Id(x => x.TvDB_ImageWideBannerID);

            Map(x => x.BannerPath);
			Map(x => x.BannerType);
			Map(x => x.BannerType2);
			Map(x => x.Enabled).Not.Nullable();
			Map(x => x.Id).Not.Nullable();
			Map(x => x.Language);
			Map(x => x.SeriesID).Not.Nullable();
			Map(x => x.SeasonNumber);
        }
	}
}
