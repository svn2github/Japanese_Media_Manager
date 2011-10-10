﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Repositories;
using JMMServer.Entities;
using System.Xml;

namespace JMMServer.Commands
{
	[Serializable]
	public class CommandRequest_GetReleaseGroup : CommandRequestImplementation, ICommandRequest
	{
		public int GroupID { get; set; }
		public bool ForceRefresh { get; set; }

		public CommandRequestPriority DefaultPriority 
		{
			get { return CommandRequestPriority.Priority9; }
		}

		public string PrettyDescription
		{
			get
			{
				return string.Format("Getting release group info from UDP API: {0}", GroupID);
			}
		}

		public CommandRequest_GetReleaseGroup()
		{
		}

		public CommandRequest_GetReleaseGroup(int grpid, bool forced)
		{
			this.GroupID = grpid;
			this.ForceRefresh = forced;
			this.CommandType = (int)CommandRequestType.AniDB_GetReleaseGroup;
			this.Priority = (int)DefaultPriority;

			GenerateCommandID();
		}

		public override void ProcessCommand()
		{
			logger.Info("Processing CommandRequest_GetReleaseGroup: {0}", GroupID);

			try
			{
				AniDB_ReleaseGroupRepository repRelGrp = new AniDB_ReleaseGroupRepository();
				AniDB_ReleaseGroup relGroup = repRelGrp.GetByGroupID(GroupID);

				if (ForceRefresh || relGroup == null)
				{
					// redownload anime details from http ap so we can get an update character list
					JMMService.AnidbProcessor.GetReleaseGroupUDP(GroupID);
				}

			}
			catch (Exception ex)
			{
				logger.Error("Error processing CommandRequest_GetReleaseGroup: {0} - {1}", GroupID, ex.ToString());
				return;
			}
		}

		public override void GenerateCommandID()
		{
			this.CommandID = string.Format("CommandRequest_GetReleaseGroup_{0}", this.GroupID);
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
				this.GroupID = int.Parse(TryGetProperty(docCreator, "CommandRequest_GetReleaseGroup", "GroupID"));
				this.ForceRefresh = bool.Parse(TryGetProperty(docCreator, "CommandRequest_GetReleaseGroup", "ForceRefresh"));
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
