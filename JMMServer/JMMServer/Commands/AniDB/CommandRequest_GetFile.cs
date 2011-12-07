﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Entities;
using System.IO;
using JMMServer.Repositories;
using JMMContracts;
using JMMFileHelper;
using AniDBAPI;
using System.Xml;
using JMMServer.WebCache;

namespace JMMServer.Commands
{
	[Serializable]
	public class CommandRequest_GetFile : CommandRequestImplementation, ICommandRequest
	{
		public int VideoLocalID { get; set; }
		private VideoLocal vlocal = null;

		public CommandRequestPriority DefaultPriority
		{
			get { return CommandRequestPriority.Priority3; }
		}

		public string PrettyDescription
		{
			get
			{
				if (vlocal != null)
					return string.Format("Getting file info from UDP API: {0}", vlocal.FullServerPath);
				else
					return string.Format("Getting file info from UDP API: {0}", VideoLocalID);
			}
		}

		public CommandRequest_GetFile()
		{
		}

		public CommandRequest_GetFile(int vidLocalID)
		{
			this.VideoLocalID = vidLocalID;
			this.CommandType = (int)CommandRequestType.AniDB_GetFileUDP;
			this.Priority = (int)DefaultPriority;

			GenerateCommandID();
		}

		public override void ProcessCommand()
		{
			logger.Info("Get AniDB file info: {0}", VideoLocalID);

			
			try
			{
				AniDB_FileRepository repAniFile = new AniDB_FileRepository();
				VideoLocalRepository repVids = new VideoLocalRepository();
				vlocal = repVids.GetByID(VideoLocalID);
				if (vlocal == null) return;

				AniDB_File aniFile = repAniFile.GetByHash(vlocal.Hash);

				Raw_AniDB_File fileInfo = JMMService.AnidbProcessor.GetFileInfo(vlocal);
				if (fileInfo != null)
				{
					// save to the database
					if (aniFile == null)
						aniFile = new AniDB_File();

					aniFile.Populate(fileInfo);

					//overwrite with local file name
					string localFileName = Path.GetFileName(vlocal.FilePath);
					aniFile.FileName = localFileName;

					repAniFile.Save(aniFile, true);
					aniFile.CreateLanguages();
					aniFile.CreateCrossEpisodes(localFileName);
				}
				
			}
			catch (Exception ex)
			{
				logger.Error("Error processing CommandRequest_GetFile: {0} - {1}", VideoLocalID, ex.ToString());
				return;
			}
		}

		/// <summary>
		/// This should generate a unique key for a command
		/// It will be used to check whether the command has already been queued before adding it
		/// </summary>
		public override void GenerateCommandID()
		{
			this.CommandID = string.Format("CommandRequest_GetFile_{0}", this.VideoLocalID);
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
				this.VideoLocalID = int.Parse(TryGetProperty(docCreator, "CommandRequest_GetFile", "VideoLocalID"));
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
