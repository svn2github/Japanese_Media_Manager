﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using JMMServer.Entities;
using JMMServer.Repositories;

namespace JMMServer
{
	public class ServerInfo : INotifyPropertyChanged
	{
		private static ServerInfo _instance;
		public static ServerInfo Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new ServerInfo();
					_instance.Init();
				}
				return _instance;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private ServerInfo()
		{
			ImportFolders = new ObservableCollection<ImportFolder>();	
		}

		private void Init()
		{
			//RefreshImportFolders();

			JMMService.CmdProcessorGeneral.OnQueueCountChangedEvent += new Commands.CommandProcessorGeneral.QueueCountChangedHandler(CmdProcessorGeneral_OnQueueCountChangedEvent);
			JMMService.CmdProcessorGeneral.OnQueueStateChangedEvent += new Commands.CommandProcessorGeneral.QueueStateChangedHandler(CmdProcessorGeneral_OnQueueStateChangedEvent);

			JMMService.CmdProcessorHasher.OnQueueCountChangedEvent += new Commands.CommandProcessorHasher.QueueCountChangedHandler(CmdProcessorHasher_OnQueueCountChangedEvent);
			JMMService.CmdProcessorHasher.OnQueueStateChangedEvent += new Commands.CommandProcessorHasher.QueueStateChangedHandler(CmdProcessorHasher_OnQueueStateChangedEvent);

			JMMService.CmdProcessorImages.OnQueueCountChangedEvent += new Commands.CommandProcessorImages.QueueCountChangedHandler(CmdProcessorImages_OnQueueCountChangedEvent);
			JMMService.CmdProcessorImages.OnQueueStateChangedEvent += new Commands.CommandProcessorImages.QueueStateChangedHandler(CmdProcessorImages_OnQueueStateChangedEvent);
		}

		void CmdProcessorImages_OnQueueStateChangedEvent(Commands.QueueStateEventArgs ev)
		{
			ImagesQueueState = ev.QueueState;
		}

		void CmdProcessorImages_OnQueueCountChangedEvent(Commands.QueueCountEventArgs ev)
		{
			ImagesQueueCount = ev.QueueCount;
		}

		void CmdProcessorHasher_OnQueueStateChangedEvent(Commands.QueueStateEventArgs ev)
		{
			HasherQueueState = ev.QueueState;
		}

		void CmdProcessorHasher_OnQueueCountChangedEvent(Commands.QueueCountEventArgs ev)
		{
			HasherQueueCount = ev.QueueCount;
		}

		void CmdProcessorGeneral_OnQueueStateChangedEvent(Commands.QueueStateEventArgs ev)
		{
			GeneralQueueState = ev.QueueState;
		}

		void CmdProcessorGeneral_OnQueueCountChangedEvent(Commands.QueueCountEventArgs ev)
		{
			GeneralQueueCount = ev.QueueCount;
		}

		#region Observable Properties

		private int hasherQueueCount = 0;
		public int HasherQueueCount
		{
			get { return hasherQueueCount; }
			set
			{
				hasherQueueCount = value;
				OnPropertyChanged(new PropertyChangedEventArgs("HasherQueueCount"));
			}
		}

		private string hasherQueueState = "";
		public string HasherQueueState
		{
			get { return hasherQueueState; }
			set
			{
				hasherQueueState = value;
				OnPropertyChanged(new PropertyChangedEventArgs("HasherQueueState"));
			}
		}

		private int imagesQueueCount = 0;
		public int ImagesQueueCount
		{
			get { return imagesQueueCount; }
			set
			{
				imagesQueueCount = value;
				OnPropertyChanged(new PropertyChangedEventArgs("ImagesQueueCount"));
			}
		}

		private string imagesQueueState = "";
		public string ImagesQueueState
		{
			get { return imagesQueueState; }
			set
			{
				imagesQueueState = value;
				OnPropertyChanged(new PropertyChangedEventArgs("ImagesQueueState"));
			}
		}

		private int generalQueueCount = 0;
		public int GeneralQueueCount
		{
			get { return generalQueueCount; }
			set
			{
				generalQueueCount = value;
				OnPropertyChanged(new PropertyChangedEventArgs("GeneralQueueCount"));
			}
		}

		private string generalQueueState = "";
		public string GeneralQueueState
		{
			get { return generalQueueState; }
			set
			{
				generalQueueState = value;
				OnPropertyChanged(new PropertyChangedEventArgs("GeneralQueueState"));
			}
		}

		private bool hasherQueuePaused = false;
		public bool HasherQueuePaused
		{
			get { return hasherQueuePaused; }
			set
			{
				hasherQueuePaused = value;
				OnPropertyChanged(new PropertyChangedEventArgs("HasherQueuePaused"));
			}
		}

		private bool hasherQueueRunning = true;
		public bool HasherQueueRunning
		{
			get { return hasherQueueRunning; }
			set
			{
				hasherQueueRunning = value;
				OnPropertyChanged(new PropertyChangedEventArgs("HasherQueueRunning"));
			}
		}

		private bool generalQueuePaused = false;
		public bool GeneralQueuePaused
		{
			get { return generalQueuePaused; }
			set
			{
				generalQueuePaused = value;
				OnPropertyChanged(new PropertyChangedEventArgs("GeneralQueuePaused"));
			}
		}

		private bool generalQueueRunning = true;
		public bool GeneralQueueRunning
		{
			get { return generalQueueRunning; }
			set
			{
				generalQueueRunning = value;
				OnPropertyChanged(new PropertyChangedEventArgs("GeneralQueueRunning"));
			}
		}

		private bool imagesQueuePaused = false;
		public bool ImagesQueuePaused
		{
			get { return imagesQueuePaused; }
			set
			{
				imagesQueuePaused = value;
				OnPropertyChanged(new PropertyChangedEventArgs("ImagesQueuePaused"));
			}
		}

		private bool imagesQueueRunning = true;
		public bool ImagesQueueRunning
		{
			get { return imagesQueueRunning; }
			set
			{
				imagesQueueRunning = value;
				OnPropertyChanged(new PropertyChangedEventArgs("ImagesQueueRunning"));
			}
		}

		private string banReason = "";
		public string BanReason
		{
			get { return banReason; }
			set
			{
				banReason = value;
				OnPropertyChanged(new PropertyChangedEventArgs("BanReason"));
			}
		}

		private string banOrigin = "";
		public string BanOrigin
		{
			get { return banOrigin; }
			set
			{
				banOrigin = value;
				OnPropertyChanged(new PropertyChangedEventArgs("BanOrigin"));
			}

		}

		private bool isBanned = false;
		public bool IsBanned
		{
			get { return isBanned; }
			set
			{
				isBanned = value;
				OnPropertyChanged(new PropertyChangedEventArgs("IsBanned"));
			}
		}

		private bool isInvalidSession = false;
		public bool IsInvalidSession
		{
			get { return isInvalidSession; }
			set
			{
				isInvalidSession = value;
				OnPropertyChanged(new PropertyChangedEventArgs("IsInvalidSession"));
			}
		}

		private bool waitingOnResponseAniDBUDP = false;
		public bool WaitingOnResponseAniDBUDP
		{
			get { return waitingOnResponseAniDBUDP; }
			set
			{
				waitingOnResponseAniDBUDP = value;
				NotWaitingOnResponseAniDBUDP = !value;
				OnPropertyChanged(new PropertyChangedEventArgs("WaitingOnResponseAniDBUDP"));
			}
		}

		private bool notWaitingOnResponseAniDBUDP = true;
		public bool NotWaitingOnResponseAniDBUDP
		{
			get { return notWaitingOnResponseAniDBUDP; }
			set
			{
				notWaitingOnResponseAniDBUDP = value;
				OnPropertyChanged(new PropertyChangedEventArgs("NotWaitingOnResponseAniDBUDP"));
			}
		}

		private string waitingOnResponseAniDBUDPString = "Idle";
		public string WaitingOnResponseAniDBUDPString
		{
			get { return waitingOnResponseAniDBUDPString; }
			set
			{
				waitingOnResponseAniDBUDPString = value;
				OnPropertyChanged(new PropertyChangedEventArgs("WaitingOnResponseAniDBUDPString"));
			}
		}

		public ObservableCollection<ImportFolder> ImportFolders { get; set; }

		public void RefreshImportFolders()
		{
			ImportFolders.Clear();

			try
			{
				ImportFolderRepository repFolders = new ImportFolderRepository();
				List<ImportFolder> fldrs = repFolders.GetAll();

				foreach (ImportFolder ifolder in fldrs)
					ImportFolders.Add(ifolder);
				
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}

		}

		#endregion
	}
}
