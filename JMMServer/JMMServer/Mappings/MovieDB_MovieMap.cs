﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class MovieDB_MovieMap : ClassMap<MovieDB_Movie>
	{
		public MovieDB_MovieMap()
        {
			Not.LazyLoad();
            Id(x => x.MovieDB_MovieID);

			Map(x => x.MovieId).Not.Nullable();
			Map(x => x.MovieName);
			Map(x => x.OriginalName);
			Map(x => x.Overview);
			
        }
	}
}
