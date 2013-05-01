﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Web;

namespace JMMClient.Downloads
{
	public class TorrentsNyaa : ITorrentSource
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private TorrentSourceType SourceType = TorrentSourceType.Nyaa;

		#region ITorrentSource Members

		public string TorrentSourceName
		{
			get
			{
				return EnumTranslator.TorrentSourceTranslated(SourceType);
			}
		}

		public string TorrentSourceNameShort
		{
			get
			{
				return EnumTranslator.TorrentSourceTranslatedShort(SourceType);
			}
		}

		public string GetSourceName()
		{
			return TorrentSourceName;
		}

		public string GetSourceNameShort()
		{
			return TorrentSourceNameShort;
		}

		public bool SupportsSearching()
		{
			return true;
		}

		public bool SupportsBrowsing()
		{
			return true;
		}

		public bool SupportsCRCMatching()
		{
			return true;
		}


		private List<TorrentLinkVM> ParseSource(string output)
		{
			List<TorrentLinkVM> torLinks = new List<TorrentLinkVM>();

			char q = (char)34;
			string quote = q.ToString();

			//class="tlistthone">Category
			//string startBlock = @"http://www.nyaa.eu/?page=torrentinfo";
			string startBlock = @"class=" + quote + "tlistthone" + quote + ">Category";

			//<td class="tlistname">
			string nameStart1 = @"<td class=" + quote + "tlistname" + quote + "><a href=";

			string nameStart2 = ">";
			string nameEnd2 = "</a>";

			string torStart = "href=" + quote;
			string torEnd = quote;



			string sizeStart = "tlistsize" + quote + ">";
			string sizeEnd = "</td>";

			string seedStart = "tlistsn" + quote + ">";
			string seedEnd = "</td>";

			string leechStart = "tlistln" + quote + ">";
			string leechEnd = "</td>";

			int pos = output.IndexOf(startBlock, 0);
			while (pos > 0)
			{

				if (pos <= 0) break;

				// find the start of the torrent
				int posBegin = output.IndexOf(nameStart1, pos + 1);
				if (posBegin <= 0) break;

				int posNameStart = output.IndexOf(nameStart2, posBegin + nameStart1.Length + 1);
				int posNameEnd = output.IndexOf(nameEnd2, posNameStart + nameStart2.Length + 1);

				string torName = output.Substring(posNameStart + nameStart2.Length, posNameEnd - posNameStart - nameStart2.Length);

				int posTorStart = output.IndexOf(torStart, posNameEnd);
				int posTorEnd = output.IndexOf(torEnd, posTorStart + torStart.Length + 1);

				string torLink = output.Substring(posTorStart + torStart.Length, posTorEnd - posTorStart - torStart.Length);
				torLink = DownloadHelper.FixNyaaTorrentLink(torLink);

				// remove html codes
				torLink = HttpUtility.HtmlDecode(torLink);

				string torSize = "";
				int posSizeStart = output.IndexOf(sizeStart, posNameEnd);
				int posSizeEnd = 0;
				if (posSizeStart > 0)
				{
					posSizeEnd = output.IndexOf(sizeEnd, posSizeStart + sizeStart.Length + 1);

					torSize = output.Substring(posSizeStart + sizeStart.Length, posSizeEnd - posSizeStart - sizeStart.Length);
				}

				string torSeed = "";
				int posSeedStart = output.IndexOf(seedStart, posSizeEnd);
				int posSeedEnd = 0;
				if (posSeedStart > 0)
				{
					posSeedEnd = output.IndexOf(seedEnd, posSeedStart + seedStart.Length + 1);

					torSeed = output.Substring(posSeedStart + seedStart.Length, posSeedEnd - posSeedStart - seedStart.Length);
				}

				string torLeech = "";
				int posLeechStart = output.IndexOf(leechStart, posSeedStart + 3);
				int posLeechEnd = 0;
				if (posLeechStart > 0)
				{
					posLeechEnd = output.IndexOf(leechEnd, posLeechStart + leechStart.Length + 1);

					torLeech = output.Substring(posLeechStart + leechStart.Length, posLeechEnd - posLeechStart - leechStart.Length);
				}

				TorrentLinkVM torrentLink = new TorrentLinkVM(SourceType);
				torrentLink.TorrentDownloadLink = torLink;
				torrentLink.TorrentName = torName;
				torrentLink.Size = torSize.Trim();
				torrentLink.Seeders = torSeed.Trim();
				torrentLink.Leechers = torLeech.Trim();
				torLinks.Add(torrentLink);

				pos = output.IndexOf(nameStart1, pos + 1);

			}
			//Console.ReadLine();

			return torLinks;
		}

		private List<TorrentLinkVM> ParseSourceSingleResult(string output)
		{
			List<TorrentLinkVM> torLinks = new List<TorrentLinkVM>();

			char q = (char)34;
			string quote = q.ToString();

			// Name:</td><td class="tinfotorrentname">
			string startBlock = @"Name:";

			// class="thead">Name:</td><td class="tinfotorrentname">[Hadena] Koi to Senkyo to Chocolate - 03 [720p] [9CD64623].mkv</td>
			string nameStart = "tinfotorrentname" + quote + ">";
			string nameEnd = "</td>";

			// Seeders:</td><td class="vtop"><span class="tinfosn">17</span>
			string seedStart = "Seeders:</td><td class=" + quote + "vtop" + quote + "><span class=" + quote + "tinfosn" + quote + ">";
			string seedEnd = "</span>";

			// Leechers:</td><td class="vtop"><span class="tinfoln">
			string leechStart = "Leechers:</td><td class=" + quote + "vtop" + quote + "><span class=" + quote + "tinfoln" + quote + ">";
			string leechEnd = "</span>";

			// File size:</td><td class="vtop">193.8 MiB</td>
			string sizeStart = "File size:</td><td class=" + quote + "vtop" + quote + ">";
			string sizeEnd = "</td>";

			// class="tinfodownloadbutton"><a href="http://www.nyaa.eu/?page=download&#38;tid=334194"
			string torStart = "class=" + quote + "tinfodownloadbutton" + quote + "><a href=" + quote;
			string torEnd = quote;


			int pos = output.IndexOf(startBlock, 0);
			while (pos > 0)
			{

				if (pos <= 0) break;

				int posNameStart = output.IndexOf(nameStart, pos + 1);
				int posNameEnd = output.IndexOf(nameEnd, posNameStart + nameStart.Length + 1);

				string torName = output.Substring(posNameStart + nameStart.Length, posNameEnd - posNameStart - nameStart.Length);

				string torSeed = "";
				int posSeedStart = output.IndexOf(seedStart, posNameEnd);
				int posSeedEnd = 0;
				if (posSeedStart > 0)
				{
					posSeedEnd = output.IndexOf(seedEnd, posSeedStart + seedStart.Length + 1);
					torSeed = output.Substring(posSeedStart + seedStart.Length, posSeedEnd - posSeedStart - seedStart.Length);
				}

				string torLeech = "";
				int posLeechStart = output.IndexOf(leechStart, posSeedEnd + 3);
				int posLeechEnd = 0;
				if (posLeechStart > 0)
				{
					posLeechEnd = output.IndexOf(leechEnd, posLeechStart + leechStart.Length + 1);
					torLeech = output.Substring(posLeechStart + leechStart.Length, posLeechEnd - posLeechStart - leechStart.Length);
				}

				string torSize = "";
				int posSizeStart = output.IndexOf(sizeStart, posLeechEnd);
				int posSizeEnd = 0;
				if (posSizeStart > 0)
				{
					posSizeEnd = output.IndexOf(sizeEnd, posSizeStart + sizeStart.Length + 1);
					torSize = output.Substring(posSizeStart + sizeStart.Length, posSizeEnd - posSizeStart - sizeStart.Length);
				}


				int posTorStart = output.IndexOf(torStart, posSizeEnd);
				int posTorEnd = output.IndexOf(torEnd, posTorStart + torStart.Length + 1);

				string torLink = output.Substring(posTorStart + torStart.Length, posTorEnd - posTorStart - torStart.Length);
				torLink = DownloadHelper.FixNyaaTorrentLink(torLink);

				// remove html codes
				torLink = HttpUtility.HtmlDecode(torLink);

				TorrentLinkVM torrentLink = new TorrentLinkVM(SourceType);
				torrentLink.TorrentDownloadLink = torLink;
				torrentLink.TorrentName = torName;
				torrentLink.Size = torSize.Trim();
				torrentLink.Seeders = torSeed.Trim();
				torrentLink.Leechers = torLeech.Trim();
				torLinks.Add(torrentLink);

				pos = output.IndexOf(startBlock, pos + 1);

			}
			//Console.ReadLine();

			return torLinks;
		}

		public List<TorrentLinkVM> GetTorrents(List<string> searchParms)
		{
			string urlBase = "http://www.nyaa.eu/?page=search&cats=1_37&filter=0&term={0}";

			string searchCriteria = "";
			foreach (string parm in searchParms)
			{
				if (searchCriteria.Length > 0) searchCriteria += "+";
				searchCriteria += parm.Trim();
			}

			string url = string.Format(urlBase, searchCriteria);
			string output = Utils.DownloadWebPage(url);

			logger.Trace("GetTorrents Search: " + url);

			// on nyaa if you search and there is only one result it goes straight to the dedicated torrent page
			if (output.Contains("Searching torrents"))
				return ParseSource(output);
			else
				return ParseSourceSingleResult(output);
		}

		public List<TorrentLinkVM> BrowseTorrents()
		{
			string url = "http://www.nyaa.eu/?page=torrents&cats=1_37";
			string output = Utils.DownloadWebPage(url);

			return ParseSource(output);
		}

		#endregion
	}
}
