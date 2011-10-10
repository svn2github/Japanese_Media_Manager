﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Repositories;
using JMMServer.Entities;
using JMMServer.WebCache;
using System.Xml;

namespace JMMServer.Commands
{
	public class CommandRequest_WebCacheSendXRefAniDBTrakt : CommandRequestImplementation, ICommandRequest
	{
		public int CrossRef_AniDB_TraktID { get; set; }

		public CommandRequestPriority DefaultPriority
		{
			get { return CommandRequestPriority.Priority9; }
		}

		public string PrettyDescription
		{
			get
			{
				return string.Format("Sending cross ref for Anidb to Trakt from web cache: {0}", CrossRef_AniDB_TraktID);
			}
		}

		public CommandRequest_WebCacheSendXRefAniDBTrakt()
		{
		}

		public CommandRequest_WebCacheSendXRefAniDBTrakt(int xrefID)
		{
			this.CrossRef_AniDB_TraktID = xrefID;
			this.CommandType = (int)CommandRequestType.WebCache_SendXRefAniDBTrakt;
			this.Priority = (int)DefaultPriority;

			GenerateCommandID();
		}

		public override void ProcessCommand()
		{
			
			try
			{
				CrossRef_AniDB_TraktRepository repCrossRef = new CrossRef_AniDB_TraktRepository();
				CrossRef_AniDB_Trakt xref = repCrossRef.GetByID(CrossRef_AniDB_TraktID);
				if (xref == null) return;

				Trakt_ShowRepository repShow = new Trakt_ShowRepository();
				Trakt_Show tvShow = repShow.GetByTraktID(xref.TraktID);
				if (tvShow == null) return;

				string showName = "";
				if (tvShow != null) showName = tvShow.Title;

				XMLService.Send_CrossRef_AniDB_Trakt(xref, showName);
			}
			catch (Exception ex)
			{
				logger.ErrorException("Error processing CommandRequest_WebCacheSendXRefAniDBTrakt: {0}" + ex.ToString(), ex);
				return;
			}
		}

		public override void GenerateCommandID()
		{
			this.CommandID = string.Format("CommandRequest_WebCacheSendXRefAniDBTrakt{0}", CrossRef_AniDB_TraktID);
		}

		public override bool LoadFromDBCommand(CommandRequest cq)
		{
			this.CommandID = cq.CommandID;
			this.CommandRequestID = cq.CommandRequestID;
			this.CommandType = cq.CommandType;
			this.Priority = cq.Priority;
			this.CommandDetails = cq.CommandDetails;
			this.DateTimeUpdated = cq.DateTimeUpdated;

			// read xml to get parameters
			if (this.CommandDetails.Trim().Length > 0)
			{
				XmlDocument docCreator = new XmlDocument();
				docCreator.LoadXml(this.CommandDetails);

				// populate the fields
				this.CrossRef_AniDB_TraktID = int.Parse(TryGetProperty(docCreator, "CommandRequest_WebCacheSendXRefAniDBTrakt", "CrossRef_AniDB_TraktID"));
			}

			return true;
		}

		public override CommandRequest ToDatabaseObject()
		{
			GenerateCommandID();

			CommandRequest cq = new CommandRequest();
			cq.CommandID = this.CommandID;
			cq.CommandType = this.CommandType;
			cq.Priority = this.Priority;
			cq.CommandDetails = this.ToXML();
			cq.DateTimeUpdated = DateTime.Now;

			return cq;
		}
	}
}
