﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class AniDB_VoteMap : ClassMap<AniDB_Vote>
	{
		public AniDB_VoteMap()
        {
			Not.LazyLoad();
			Id(x => x.AniDB_VoteID);

			Map(x => x.EntityID).Not.Nullable();
			Map(x => x.VoteValue).Not.Nullable();
			Map(x => x.VoteType).Not.Nullable();
        }
	}
}
