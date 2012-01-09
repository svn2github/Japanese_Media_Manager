﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JMMServer.Entities;
using JMMServer.Commands.WebCache;
using JMMServer.Commands.MAL;

namespace JMMServer.Commands
{
	public class CommandHelper
	{
		// List of Default priorities for commands
		// Pri 1
		//------
		// Reserved for commands user manually initiates from UI
		//------
		// Pri 2
		//------
		// CommandRequest_GetAnimeHTTP
		//------
		// Pri 3
		//------
		// CommandRequest_ProcessFile
		// CommandRequest_GetFile
		//------
		// Pri 4
		//------
		// CommandRequest_GetUpdated
		// CommandRequest_ReadMediaInfo
		//------
		// Pri 5
		//------
		// CommandRequest_GetReleaseGroupStatus
		//------
		// Pri 6
		//------
		// CommandRequest_SyncMyList
		// CommandRequest_SyncMyVotes
		//------
		// Pri 7
		//------
		// CommandRequest_GetCalendar
		//------
		// Pri 8
		//------
		// CommandRequest_UpdateMyListFileStatus
		// CommandRequest_GetCharactersCreators
		// CommandRequest_TraktSyncCollection
		// CommandRequest_TvDBUpdateSeriesAndEpisodes
		// CommandRequest_TvDBDownloadImages
		// CommandRequest_TvDBSearchAnime
		// CommandRequest_MovieDBSearchAnime
		// CommandRequest_TraktSearchAnime
		// CommandRequest_MALSearchAnime
		//------
		// Pri 9
		//------
		// CommandRequest_WebCacheSendFileHash
		// CommandRequest_GetReviews
		// CommandRequest_GetReleaseGroup
		// CommandRequest_WebCacheSendXRefFileEpisode
		// CommandRequest_WebCacheDeleteXRefFileEpisode
		// CommandRequest_AddFileToMyList
		// CommandRequest_DeleteFileFromMyList
		// CommandRequest_VoteAnime
		// CommandRequest_WebCacheDeleteXRefAniDBTvDB
		// CommandRequest_WebCacheDeleteXRefAniDBTvDBAll
		// CommandRequest_WebCacheSendXRefAniDBTvDB
		// CommandRequest_WebCacheSendXRefAniDBOther
		// CommandRequest_WebCacheDeleteXRefAniDBOther
		// CommandRequest_WebCacheDeleteXRefAniDBTrakt
		// CommandRequest_WebCacheSendXRefAniDBTrakt
		// CommandRequest_TraktUpdateInfoAndImages
		// CommandRequest_TraktShowScrobble
		// CommandRequest_TraktSyncCollectionSeries
		// CommandRequest_TraktShowEpisodeUnseen
		// CommandRequest_DownloadImage
		// CommandRequest_TraktUpdateAllSeries


		public static ICommandRequest GetCommand(CommandRequest crdb)
		{
			CommandRequestType crt = (CommandRequestType)crdb.CommandType;
			switch (crt)
			{
				case CommandRequestType.MAL_SearchAnime:
					CommandRequest_MALSearchAnime cr_MAL_SearchAnime = new CommandRequest_MALSearchAnime();
					cr_MAL_SearchAnime.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_MAL_SearchAnime;

				case CommandRequestType.WebCache_SendXRefAniDBMAL:
					CommandRequest_WebCacheSendXRefAniDBMAL cr_WebCacheSendXRefAniDBMAL = new CommandRequest_WebCacheSendXRefAniDBMAL();
					cr_WebCacheSendXRefAniDBMAL.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_WebCacheSendXRefAniDBMAL;

				case CommandRequestType.WebCache_DeleteXRefAniDBMAL:
					CommandRequest_WebCacheDeleteXRefAniDBMAL cr_WebCacheDeleteXRefAniDBMAL = new CommandRequest_WebCacheDeleteXRefAniDBMAL();
					cr_WebCacheDeleteXRefAniDBMAL.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_WebCacheDeleteXRefAniDBMAL;

				case CommandRequestType.AniDB_GetFileUDP:
					CommandRequest_GetFile cr_AniDB_GetFileUDP = new CommandRequest_GetFile();
					cr_AniDB_GetFileUDP.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_AniDB_GetFileUDP;

				case CommandRequestType.ReadMediaInfo:
					CommandRequest_ReadMediaInfo cr_ReadMediaInfo = new CommandRequest_ReadMediaInfo();
					cr_ReadMediaInfo.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_ReadMediaInfo;

				case CommandRequestType.Trakt_UpdateAllSeries:
					CommandRequest_TraktUpdateAllSeries cr_Trakt_UpdateAllSeries = new CommandRequest_TraktUpdateAllSeries();
					cr_Trakt_UpdateAllSeries.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_UpdateAllSeries;

				case CommandRequestType.Trakt_ShowEpisodeUnseen:
					CommandRequest_TraktShowEpisodeUnseen cr_Trakt_ShowEpisodeUnseen = new CommandRequest_TraktShowEpisodeUnseen();
					cr_Trakt_ShowEpisodeUnseen.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_ShowEpisodeUnseen;

				case CommandRequestType.Trakt_SyncCollectionSeries:
					CommandRequest_TraktSyncCollectionSeries cr_Trakt_SyncCollectionSeries = new CommandRequest_TraktSyncCollectionSeries();
					cr_Trakt_SyncCollectionSeries.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_SyncCollectionSeries;

				case CommandRequestType.Trakt_SyncCollection:
					CommandRequest_TraktSyncCollection cr_Trakt_SyncCollection = new CommandRequest_TraktSyncCollection();
					cr_Trakt_SyncCollection.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_SyncCollection;

				case CommandRequestType.Trakt_ShowScrobble:
					CommandRequest_TraktShowScrobble cr_Trakt_ShowScrobble = new CommandRequest_TraktShowScrobble();
					cr_Trakt_ShowScrobble.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_ShowScrobble;

				case CommandRequestType.Trakt_UpdateInfoImages:
					CommandRequest_TraktUpdateInfoAndImages cr_Trakt_UpdateInfoImages = new CommandRequest_TraktUpdateInfoAndImages();
					cr_Trakt_UpdateInfoImages.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_UpdateInfoImages;

				case CommandRequestType.WebCache_SendXRefAniDBTrakt:
					CommandRequest_WebCacheSendXRefAniDBTrakt cr_WebCache_SendXRefAniDBTrakt = new CommandRequest_WebCacheSendXRefAniDBTrakt();
					cr_WebCache_SendXRefAniDBTrakt.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_WebCache_SendXRefAniDBTrakt;

				case CommandRequestType.WebCache_DeleteXRefAniDBTrakt:
					CommandRequest_WebCacheDeleteXRefAniDBTrakt cr_WebCache_DeleteXRefAniDBTrakt = new CommandRequest_WebCacheDeleteXRefAniDBTrakt();
					cr_WebCache_DeleteXRefAniDBTrakt.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_WebCache_DeleteXRefAniDBTrakt;

				case CommandRequestType.Trakt_SearchAnime:
					CommandRequest_TraktSearchAnime cr_Trakt_SearchAnime = new CommandRequest_TraktSearchAnime();
					cr_Trakt_SearchAnime.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_Trakt_SearchAnime;

				case CommandRequestType.MovieDB_SearchAnime:
					CommandRequest_MovieDBSearchAnime cr_MovieDB_SearchAnime = new CommandRequest_MovieDBSearchAnime();
					cr_MovieDB_SearchAnime.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_MovieDB_SearchAnime;

				case CommandRequestType.WebCache_DeleteXRefAniDBOther:
					CommandRequest_WebCacheDeleteXRefAniDBOther cr_SendXRefAniDBOther = new CommandRequest_WebCacheDeleteXRefAniDBOther();
					cr_SendXRefAniDBOther.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SendXRefAniDBOther;

				case CommandRequestType.WebCache_SendXRefAniDBOther:
					CommandRequest_WebCacheSendXRefAniDBOther cr_WebCacheSendXRefAniDBOther = new CommandRequest_WebCacheSendXRefAniDBOther();
					cr_WebCacheSendXRefAniDBOther.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_WebCacheSendXRefAniDBOther;

				case CommandRequestType.AniDB_DeleteFileUDP:
					CommandRequest_DeleteFileFromMyList cr_AniDB_DeleteFileUDP = new CommandRequest_DeleteFileFromMyList();
					cr_AniDB_DeleteFileUDP.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_AniDB_DeleteFileUDP;

				case CommandRequestType.ImageDownload:
					CommandRequest_DownloadImage cr_ImageDownload = new CommandRequest_DownloadImage();
					cr_ImageDownload.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_ImageDownload;

				case CommandRequestType.WebCache_DeleteXRefAniDBTvDB:
					CommandRequest_WebCacheDeleteXRefAniDBTvDB cr_DeleteXRefAniDBTvDB = new CommandRequest_WebCacheDeleteXRefAniDBTvDB();
					cr_DeleteXRefAniDBTvDB.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_DeleteXRefAniDBTvDB;

				case CommandRequestType.WebCache_DeleteXRefTvDB:
					CommandRequest_WebCacheDeleteXRefAniDBTvDBAll cr_DeleteXRefTvDB = new CommandRequest_WebCacheDeleteXRefAniDBTvDBAll();
					cr_DeleteXRefTvDB.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_DeleteXRefTvDB;

				case CommandRequestType.WebCache_SendXRefAniDBTvDB:
					CommandRequest_WebCacheSendXRefAniDBTvDB cr_SendXRefAniDBTvDB = new CommandRequest_WebCacheSendXRefAniDBTvDB();
					cr_SendXRefAniDBTvDB.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SendXRefAniDBTvDB;


				case CommandRequestType.TvDB_SearchAnime:
					CommandRequest_TvDBSearchAnime cr_TvDB_SearchAnime = new CommandRequest_TvDBSearchAnime();
					cr_TvDB_SearchAnime.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_TvDB_SearchAnime;

				case CommandRequestType.TvDB_DownloadImages:
					CommandRequest_TvDBDownloadImages cr_TvDB_DownloadImages = new CommandRequest_TvDBDownloadImages();
					cr_TvDB_DownloadImages.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_TvDB_DownloadImages;

				case CommandRequestType.TvDB_SeriesEpisodes:
					CommandRequest_TvDBUpdateSeriesAndEpisodes cr_TvDB_Episodes = new CommandRequest_TvDBUpdateSeriesAndEpisodes();
					cr_TvDB_Episodes.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_TvDB_Episodes;

				case CommandRequestType.AniDB_SyncVotes:
					CommandRequest_SyncMyVotes cr_SyncVotes = new CommandRequest_SyncMyVotes();
					cr_SyncVotes.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SyncVotes;

				case CommandRequestType.AniDB_VoteAnime:
					CommandRequest_VoteAnime cr_VoteAnime = new CommandRequest_VoteAnime();
					cr_VoteAnime.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_VoteAnime;

				case CommandRequestType.AniDB_GetCalendar:
					CommandRequest_GetCalendar cr_GetCalendar = new CommandRequest_GetCalendar();
					cr_GetCalendar.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_GetCalendar;

				case CommandRequestType.AniDB_GetReleaseGroup:
					CommandRequest_GetReleaseGroup cr_GetReleaseGroup = new CommandRequest_GetReleaseGroup();
					cr_GetReleaseGroup.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_GetReleaseGroup;

				case CommandRequestType.AniDB_GetAnimeHTTP:
					CommandRequest_GetAnimeHTTP cr_geth = new CommandRequest_GetAnimeHTTP();
					cr_geth.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_geth;

				case CommandRequestType.AniDB_GetReleaseGroupStatus:
					CommandRequest_GetReleaseGroupStatus cr_GetReleaseGroupStatus = new CommandRequest_GetReleaseGroupStatus();
					cr_GetReleaseGroupStatus.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_GetReleaseGroupStatus;

				case CommandRequestType.HashFile:
					CommandRequest_HashFile cr_HashFile = new CommandRequest_HashFile();
					cr_HashFile.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_HashFile;

				case CommandRequestType.ProcessFile:
					CommandRequest_ProcessFile cr_pf = new CommandRequest_ProcessFile();
					cr_pf.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_pf;

				case CommandRequestType.AniDB_AddFileUDP:
					CommandRequest_AddFileToMyList cr_af = new CommandRequest_AddFileToMyList();
					cr_af.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_af;

				case CommandRequestType.AniDB_UpdateWatchedUDP:
					CommandRequest_UpdateMyListFileStatus cr_umlf = new CommandRequest_UpdateMyListFileStatus();
					cr_umlf.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_umlf;

				case CommandRequestType.WebCache_SendFileHash:
					CommandRequest_WebCacheSendFileHash cr_SendFileHash = new CommandRequest_WebCacheSendFileHash();
					cr_SendFileHash.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SendFileHash;

				case CommandRequestType.WebCache_DeleteXRefFileEpisode:
					CommandRequest_WebCacheDeleteXRefFileEpisode cr_DeleteXRefFileEpisode = new CommandRequest_WebCacheDeleteXRefFileEpisode();
					cr_DeleteXRefFileEpisode.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_DeleteXRefFileEpisode;

				case CommandRequestType.WebCache_SendXRefFileEpisode:
					CommandRequest_WebCacheSendXRefFileEpisode cr_SendXRefFileEpisode = new CommandRequest_WebCacheSendXRefFileEpisode();
					cr_SendXRefFileEpisode.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SendXRefFileEpisode;
					
				case CommandRequestType.AniDB_GetReviews:
					CommandRequest_GetReviews cr_GetReviews = new CommandRequest_GetReviews();
					cr_GetReviews.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_GetReviews;

				case CommandRequestType.AniDB_GetUpdated:
					CommandRequest_GetUpdated cr_GetUpdated = new CommandRequest_GetUpdated();
					cr_GetUpdated.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_GetUpdated;

				case CommandRequestType.AniDB_SyncMyList:
					CommandRequest_SyncMyList cr_SyncMyList = new CommandRequest_SyncMyList();
					cr_SyncMyList.LoadFromDBCommand(crdb);
					return (ICommandRequest)cr_SyncMyList;
			}

			return null;
		}

		
	}

	
}
