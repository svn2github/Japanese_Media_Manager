﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Entities;
using NHibernate.Criterion;
using NHibernate;

namespace JMMServer.Repositories
{
	public class TvDB_ImageFanartRepository
	{
		public void Save(TvDB_ImageFanart obj)
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				// populate the database
				using (var transaction = session.BeginTransaction())
				{
					session.SaveOrUpdate(obj);
					transaction.Commit();
				}
			}
		}

		public TvDB_ImageFanart GetByID(int id)
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				return GetByID(session, id);
			}
		}

		public TvDB_ImageFanart GetByID(ISession session, int id)
		{
			return session.Get<TvDB_ImageFanart>(id);
		}

		public TvDB_ImageFanart GetByTvDBID(int id)
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				TvDB_ImageFanart cr = session
					.CreateCriteria(typeof(TvDB_ImageFanart))
					.Add(Restrictions.Eq("Id", id))
					.UniqueResult<TvDB_ImageFanart>();
				return cr;
			}
		}

		public List<TvDB_ImageFanart> GetBySeriesID(int seriesID)
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				return GetBySeriesID(session, seriesID);
			}
		}

		public List<TvDB_ImageFanart> GetBySeriesID(ISession session, int seriesID)
		{
			var objs = session
				.CreateCriteria(typeof(TvDB_ImageFanart))
				.Add(Restrictions.Eq("SeriesID", seriesID))
				.List<TvDB_ImageFanart>();

			return new List<TvDB_ImageFanart>(objs);
		}

		public List<TvDB_ImageFanart> GetAll()
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				var objs = session
					.CreateCriteria(typeof(TvDB_ImageFanart))
					.List<TvDB_ImageFanart>();

				return new List<TvDB_ImageFanart>(objs);
			}
		}

		public void Delete(int id)
		{
			using (var session = JMMService.SessionFactory.OpenSession())
			{
				// populate the database
				using (var transaction = session.BeginTransaction())
				{
					TvDB_ImageFanart cr = GetByID(id);
					if (cr != null)
					{
						session.Delete(cr);
						transaction.Commit();
					}
				}
			}
		}
	}
}
