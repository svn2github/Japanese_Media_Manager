﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel.Web;
using JMMContracts;
using JMMServer.Repositories;
using JMMServer.Entities;
using NLog;

namespace JMMServer
{
	public class JMMServiceImplementationREST : IJMMServerREST
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public Stream GetImage(string ImageType, string ImageID)
		{
			AniDB_AnimeRepository repAnime = new AniDB_AnimeRepository();
			TvDB_ImagePosterRepository repPosters = new TvDB_ImagePosterRepository();
			TvDB_EpisodeRepository repEpisodes = new TvDB_EpisodeRepository();
			TvDB_ImageFanartRepository repFanart = new TvDB_ImageFanartRepository();
			TvDB_ImageWideBannerRepository repWideBanners = new TvDB_ImageWideBannerRepository();

			MovieDB_PosterRepository repMoviePosters = new MovieDB_PosterRepository();
			MovieDB_FanartRepository repMovieFanart = new MovieDB_FanartRepository();

			Trakt_ImageFanartRepository repTraktFanart = new Trakt_ImageFanartRepository();
			Trakt_ImagePosterRepository repTraktPosters = new Trakt_ImagePosterRepository();
			Trakt_EpisodeRepository repTraktEpisodes = new Trakt_EpisodeRepository();
			Trakt_FriendRepository repTraktFriends = new Trakt_FriendRepository();

			JMMImageType imageType = (JMMImageType)int.Parse(ImageType);

			switch (imageType)
			{
				case JMMImageType.AniDB_Cover:

					AniDB_Anime anime = repAnime.GetByAnimeID(int.Parse(ImageID));
					if (anime == null) return null;

					if (File.Exists(anime.PosterPath))
					{
						FileStream fs = File.OpenRead(anime.PosterPath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find AniDB_Cover image: {0}", anime.PosterPath);
						return null;
					}

				case JMMImageType.AniDB_Character:

					AniDB_CharacterRepository repChar = new AniDB_CharacterRepository();
					AniDB_Character chr = repChar.GetByID(int.Parse(ImageID));
					if (chr == null) return null;

					if (File.Exists(chr.PosterPath))
					{
						FileStream fs = File.OpenRead(chr.PosterPath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find AniDB_Character image: {0}", chr.PosterPath);
						return null;
					}

				case JMMImageType.AniDB_Creator:

					AniDB_SeiyuuRepository repCreator = new AniDB_SeiyuuRepository();
					AniDB_Seiyuu creator = repCreator.GetByID(int.Parse(ImageID));
					if (creator == null) return null;

					if (File.Exists(creator.PosterPath))
					{
						FileStream fs = File.OpenRead(creator.PosterPath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find AniDB_Creator image: {0}", creator.PosterPath);
						return null;
					}

				case JMMImageType.TvDB_Cover:

					TvDB_ImagePoster poster = repPosters.GetByID(int.Parse(ImageID));
					if (poster == null) return null;

					if (File.Exists(poster.FullImagePath))
					{
						FileStream fs = File.OpenRead(poster.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find TvDB_Cover image: {0}", poster.FullImagePath);
						return null;
					}

				case JMMImageType.TvDB_Banner:

					TvDB_ImageWideBanner wideBanner = repWideBanners.GetByID(int.Parse(ImageID));
					if (wideBanner == null) return null;

					if (File.Exists(wideBanner.FullImagePath))
					{
						FileStream fs = File.OpenRead(wideBanner.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find TvDB_Banner image: {0}", wideBanner.FullImagePath);
						return null;
					}

				case JMMImageType.TvDB_Episode:

					TvDB_Episode ep = repEpisodes.GetByID(int.Parse(ImageID));
					if (ep == null) return null;

					if (File.Exists(ep.FullImagePath))
					{
						FileStream fs = File.OpenRead(ep.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find TvDB_Episode image: {0}", ep.FullImagePath);
						return null;
					}

				case JMMImageType.TvDB_FanArt:

					TvDB_ImageFanart fanart = repFanart.GetByID(int.Parse(ImageID));
					if (fanart == null) return null;

					if (File.Exists(fanart.FullImagePath))
					{
						FileStream fs = File.OpenRead(fanart.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find TvDB_FanArt image: {0}", fanart.FullImagePath);
						return null;
					}

				case JMMImageType.MovieDB_Poster:

					MovieDB_Poster mPoster = repMoviePosters.GetByID(int.Parse(ImageID));
					if (mPoster == null) return null;

					// now find only the original size
					mPoster = repMoviePosters.GetByOnlineID(mPoster.ImageID, Constants.MovieDBImageSize.Original);
					if (mPoster == null) return null;

					if (File.Exists(mPoster.FullImagePath))
					{
						FileStream fs = File.OpenRead(mPoster.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find MovieDB_Poster image: {0}", mPoster.FullImagePath);
						return null;
					}

				case JMMImageType.MovieDB_FanArt:

					MovieDB_Fanart mFanart = repMovieFanart.GetByID(int.Parse(ImageID));
					if (mFanart == null) return null;

					mFanart = repMovieFanart.GetByOnlineID(mFanart.ImageID, Constants.MovieDBImageSize.Original);
					if (mFanart == null) return null;

					if (File.Exists(mFanart.FullImagePath))
					{
						FileStream fs = File.OpenRead(mFanart.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find MovieDB_FanArt image: {0}", mFanart.FullImagePath);
						return null;
					}

				case JMMImageType.Trakt_Fanart:

					Trakt_ImageFanart tFanart = repTraktFanart.GetByID(int.Parse(ImageID));
					if (tFanart == null) return null;

					if (File.Exists(tFanart.FullImagePath))
					{
						FileStream fs = File.OpenRead(tFanart.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find Trakt_Fanart image: {0}", tFanart.FullImagePath);
						return null;
					}

				case JMMImageType.Trakt_Friend:


					Trakt_Friend tFriend = repTraktFriends.GetByID(int.Parse(ImageID));
					if (tFriend == null) return null;

					if (File.Exists(tFriend.FullImagePath))
					{
						FileStream fs = File.OpenRead(tFriend.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find Trakt_Friend image: {0}", tFriend.FullImagePath);
						return null;
					}

				case JMMImageType.Trakt_ActivityScrobble:
				case JMMImageType.Trakt_ShoutUser:


					Trakt_Friend tFriendScrobble = repTraktFriends.GetByID(int.Parse(ImageID));
					if (tFriendScrobble == null) return null;

					if (File.Exists(tFriendScrobble.FullImagePath))
					{
						FileStream fs = File.OpenRead(tFriendScrobble.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find Trakt_ActivityScrobble image: {0}", tFriendScrobble.FullImagePath);
						return null;
					}

				case JMMImageType.Trakt_Poster:

					Trakt_ImagePoster tPoster = repTraktPosters.GetByID(int.Parse(ImageID));
					if (tPoster == null) return null;

					if (File.Exists(tPoster.FullImagePath))
					{
						FileStream fs = File.OpenRead(tPoster.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find Trakt_Poster image: {0}", tPoster.FullImagePath);
						return null;
					}

				case JMMImageType.Trakt_Episode:
				case JMMImageType.Trakt_WatchedEpisode:

					Trakt_Episode tEpisode = repTraktEpisodes.GetByID(int.Parse(ImageID));
					if (tEpisode == null) return null;

					if (File.Exists(tEpisode.FullImagePath))
					{
						FileStream fs = File.OpenRead(tEpisode.FullImagePath);
						WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
						return fs;
					}
					else
					{
						logger.Trace("Could not find Trakt_Episode image: {0}", tEpisode.FullImagePath);
						return null;
					}

				default:

					return null;
			}



			
		}

		public Stream GetImageUsingPath(string serverImagePath)
		{
			if (File.Exists(serverImagePath))
			{
				FileStream fs = File.OpenRead(serverImagePath);
				WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
				return fs;
			}
			else
			{
				logger.Trace("Could not find image: {0}", serverImagePath);
				return null;
			}
		}
	}
}
