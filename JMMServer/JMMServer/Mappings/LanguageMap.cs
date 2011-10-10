﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using JMMServer.Entities;

namespace JMMServer.Mappings
{
	public class LanguageMap : ClassMap<Language>
	{
		public LanguageMap()
        {
			Not.LazyLoad();
            Id(x => x.LanguageID);

			Map(x => x.LanguageName).Not.Nullable();
        }
	}
}
