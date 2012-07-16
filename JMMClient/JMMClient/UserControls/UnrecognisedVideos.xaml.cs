﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Collections.ObjectModel;
using JMMClient.ViewModel;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using JMMClient.Forms;

namespace JMMClient.UserControls
{
	/// <summary>
	/// Interaction logic for UnrecognisedVideos.xaml
	/// </summary>
	public partial class UnrecognisedVideos : UserControl
	{
		

		public ICollectionView ViewFiles { get; set; }
		public ObservableCollection<VideoLocalVM> UnrecognisedFiles { get; set; }

		public ICollectionView ViewSeries { get; set; }
		public ObservableCollection<AnimeSeriesVM> AllSeries { get; set; }

		public static readonly DependencyProperty AnyVideosSelectedProperty = DependencyProperty.Register("AnyVideosSelected",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty OneVideoSelectedProperty = DependencyProperty.Register("OneVideoSelected",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty OneVideoTypeRangeProperty = DependencyProperty.Register("OneVideoTypeRange",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty OneVideoTypeSingleProperty = DependencyProperty.Register("OneVideoTypeSingle",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty MultipleVideosSelectedProperty = DependencyProperty.Register("MultipleVideosSelected",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty MultipleTypeRangeProperty = DependencyProperty.Register("MultipleTypeRange",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty MultipleTypeSingleProperty = DependencyProperty.Register("MultipleTypeSingle",
			typeof(bool), typeof(UnrecognisedVideos), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty FileCountProperty = DependencyProperty.Register("FileCount",
			typeof(int), typeof(UnrecognisedVideos), new UIPropertyMetadata(0, null));

		public int FileCount
		{
			get { return (int)GetValue(FileCountProperty); }
			set { SetValue(FileCountProperty, value); }
		}

		public bool AnyVideosSelected
		{
			get { return (bool)GetValue(AnyVideosSelectedProperty); }
			set { SetValue(AnyVideosSelectedProperty, value); }
		}

		public bool OneVideoSelected
		{
			get { return (bool)GetValue(OneVideoSelectedProperty); }
			set { SetValue(OneVideoSelectedProperty, value); }
		}

		public bool OneVideoTypeRange
		{
			get { return (bool)GetValue(OneVideoTypeRangeProperty); }
			set { SetValue(OneVideoTypeRangeProperty, value); }
		}

		public bool OneVideoTypeSingle
		{
			get { return (bool)GetValue(OneVideoTypeSingleProperty); }
			set { SetValue(OneVideoTypeSingleProperty, value); }
		}

		public bool MultipleVideosSelected
		{
			get { return (bool)GetValue(MultipleVideosSelectedProperty); }
			set { SetValue(MultipleVideosSelectedProperty, value); }
		}

		public bool MultipleTypeRange
		{
			get { return (bool)GetValue(MultipleTypeRangeProperty); }
			set { SetValue(MultipleTypeRangeProperty, value); }
		}

		public bool MultipleTypeSingle
		{
			get { return (bool)GetValue(MultipleTypeSingleProperty); }
			set { SetValue(MultipleTypeSingleProperty, value); }
		}

		public UnrecognisedVideos()
		{
			InitializeComponent();

			UnrecognisedFiles = new ObservableCollection<VideoLocalVM>();
			ViewFiles = CollectionViewSource.GetDefaultView(UnrecognisedFiles);
			ViewFiles.SortDescriptions.Add(new SortDescription("FullPath", ListSortDirection.Ascending));
			ViewFiles.Filter = FileSearchFilter;

			AllSeries = new ObservableCollection<AnimeSeriesVM>();
			ViewSeries = CollectionViewSource.GetDefaultView(AllSeries);
			ViewSeries.SortDescriptions.Add(new SortDescription("SeriesName", ListSortDirection.Ascending));
			ViewSeries.Filter = SeriesSearchFilter;


			btnRefresh.Click += new RoutedEventHandler(btnRefresh_Click);
			btnConfirm.Click += new RoutedEventHandler(btnConfirm_Click);
			btnAddSeries.Click += new RoutedEventHandler(btnAddSeries_Click);
			btnRescan.Click += new RoutedEventHandler(btnRescan_Click);

			txtSeriesSearch.TextChanged += new TextChangedEventHandler(txtSeriesSearch_TextChanged);
			

			lbSeries.SelectionChanged += new SelectionChangedEventHandler(lbSeries_SelectionChanged);
			cboEpisodes.SelectionChanged += new SelectionChangedEventHandler(cboEpisodes_SelectionChanged);
			lbVideos.SelectionChanged += new SelectionChangedEventHandler(lbVideos_SelectionChanged);
			txtStartEpNum.TextChanged += new TextChangedEventHandler(txtStartEpNum_TextChanged);
			txtEndEpNumSingle.TextChanged += new TextChangedEventHandler(txtEndEpNumSingle_TextChanged);

			cboMultiType.Items.Add(Properties.Resources.MultiTypeRange);
			cboMultiType.Items.Add(Properties.Resources.MultiTypeSingle);
			cboMultiType.SelectedIndex = 1;

			cboMultiType.SelectionChanged += new SelectionChangedEventHandler(cboMultiType_SelectionChanged);

			SetConfirmDetails();

			OneVideoSelected = lbVideos.SelectedItems.Count == 1;
			MultipleVideosSelected = lbVideos.SelectedItems.Count > 1;

			btnClearSearch.Click += new RoutedEventHandler(btnClearSearch_Click);
			txtFileSearch.TextChanged += new TextChangedEventHandler(txtFileSearch_TextChanged);
			btnLogs.Click += new RoutedEventHandler(btnLogs_Click);

			btnRefreshSeriesList.Click += new RoutedEventHandler(btnRefreshSeriesList_Click);
		}

		void btnRefreshSeriesList_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!JMMServerVM.Instance.ServerOnline) return;

				this.Cursor = Cursors.Wait;
				RefreshSeries();
				this.Cursor = Cursors.Arrow;

			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
			finally
			{
				this.Cursor = Cursors.Arrow;
			}

			
		}

		void btnLogs_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
				string logPath = Path.Combine(appPath, "logs");

				Process.Start(new ProcessStartInfo(logPath));
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		void btnRescan_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!JMMServerVM.Instance.ServerOnline) return;

				this.Cursor = Cursors.Wait;
				JMMServerVM.Instance.clientBinaryHTTP.RescanUnlinkedFiles();
				this.Cursor = Cursors.Arrow;

				MessageBox.Show("Files queued for AniDB scan", "Complete", MessageBoxButton.OK, MessageBoxImage.Information);

			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
			finally
			{
				this.Cursor = Cursors.Arrow;
			}
		}

		void txtFileSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			ViewFiles.Refresh();
		}

		void btnClearSearch_Click(object sender, RoutedEventArgs e)
		{
			txtFileSearch.Text = "";
		}

		void txtEndEpNumSingle_TextChanged(object sender, TextChangedEventArgs e)
		{
			SetConfirmDetails();
		}

		void cboMultiType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SetConfirmDetails();
		}

		void btnAddSeries_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				NewSeries frm = new NewSeries();
				frm.Owner = GetTopParent();
				frm.Init(null, "");
				bool? result = frm.ShowDialog();
				if (result.HasValue && result.Value == true)
				{
					RefreshSeries();

					AnimeSeriesVM ser = frm.AnimeSeries;
					txtSeriesSearch.Text = ser.AniDB_Anime.FormattedTitle;
				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		private Window GetTopParent()
		{
			DependencyObject dpParent = this.Parent;
			do
			{
				dpParent = LogicalTreeHelper.GetParent(dpParent);
			}
			while (dpParent.GetType().BaseType != typeof(Window));

			return dpParent as Window;
		}

		private void EnableDisableControls(bool val)
		{
			lbSeries.IsEnabled = val;
			lbVideos.IsEnabled = val;
			btnAddSeries.IsEnabled = val;
			btnConfirm.IsEnabled = val;
			btnRefresh.IsEnabled = val;
			txtSeriesSearch.IsEnabled = val;
			txtStartEpNum.IsEnabled = val;
			cboEpisodes.IsEnabled = val;
			ccDetail.IsEnabled = val;
			ccDetailMultiple.IsEnabled = val;
		}

		void btnConfirm_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Window parentWindow = Window.GetWindow(this);

				// if only one video selected
				if (OneVideoSelected)
				{
					EnableDisableControls(false);

					VideoLocalVM vid = lbVideos.SelectedItem as VideoLocalVM;

					if (cboMultiType.SelectedIndex == 0)
					{
						// single file to multiple episodes
						// eg a file is a double episode

						int startEpNum = 0, endEpNum = 0;

						int.TryParse(txtStartEpNum.Text, out startEpNum);
						int.TryParse(txtEndEpNumSingle.Text, out endEpNum);

						string result = "";
						// make sure the episode range is valid
						// make sure the last episode number is within the valid range
						AnimeSeriesVM series = lbSeries.SelectedItem as AnimeSeriesVM;
						if (series.LatestRegularEpisodeNumber < endEpNum || startEpNum <= 0 && endEpNum <= 0 && endEpNum <= startEpNum)
						{
							// otherwise allow the user to refresh it from anidb
							MessageBoxResult res = MessageBox.Show(Properties.Resources.MSG_ERR_InvalidEpGetAnime, "Error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
							if (res == MessageBoxResult.Yes)
							{
								result = JMMServerVM.Instance.clientBinaryHTTP.UpdateAnimeData(series.AniDB_ID);
								if (result.Length > 0)
								{
									MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
									EnableDisableControls(true);
									return;
								}
								else
								{
									// check again
									if (series.LatestRegularEpisodeNumber < endEpNum)
									{
										MessageBox.Show(Properties.Resources.MSG_ERR_InvalidEp, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
										EnableDisableControls(true);
										return;
									}

								}
							}
							else
							{
								EnableDisableControls(true);
								return;
							}
						}

						result = JMMServerVM.Instance.clientBinaryHTTP.AssociateSingleFileWithMultipleEpisodes(vid.VideoLocalID, series.AnimeSeriesID.Value, startEpNum, endEpNum);
						if (result.Length > 0)
						{
							MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
						else
						{
							RefreshUnrecognisedFiles();
						}
					}
					else
					{
						// single file to a single episode
						AnimeEpisodeVM ep = cboEpisodes.SelectedItem as AnimeEpisodeVM;

						string result = JMMServerVM.Instance.clientBinaryHTTP.AssociateSingleFile(vid.VideoLocalID, ep.AnimeEpisodeID);
						if (result.Length > 0)
						{
							MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						}
						else
						{
							RefreshUnrecognisedFiles();
							//MainListHelperVM.Instance.UpdateHeirarchy(ep, ((MainWindow)parentWindow).epListMain);
							MainListHelperVM.Instance.UpdateHeirarchy(ep);
						}
					}
				}

				// if multiple videos selected
				if (MultipleVideosSelected)
				{
					int startEpNum = 0, endEpNum = 0;
					
					int.TryParse(txtStartEpNum.Text, out startEpNum);

					if (MultipleTypeRange)
						endEpNum = startEpNum + lbVideos.SelectedItems.Count - 1;
					else
						endEpNum = startEpNum;

					// make sure the last episode number is within the valid range
					AnimeSeriesVM series = lbSeries.SelectedItem as AnimeSeriesVM;
					if (series.LatestRegularEpisodeNumber < endEpNum && startEpNum > 0)
					{
						// otherwise allow the user to refresh it from anidb
						MessageBoxResult res = MessageBox.Show(Properties.Resources.MSG_ERR_InvalidEpGetAnime, "Error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
						if (res == MessageBoxResult.Yes)
						{
							string result = JMMServerVM.Instance.clientBinaryHTTP.UpdateAnimeData(series.AniDB_ID);
							if (result.Length > 0)
							{
								MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
								EnableDisableControls(true);
								return;
							}
							else
							{
								// check again
								if (series.LatestRegularEpisodeNumber < endEpNum)
								{
									MessageBox.Show(Properties.Resources.MSG_ERR_InvalidEp, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
									EnableDisableControls(true);
									return;
								}

							}
						}
						else
						{
							EnableDisableControls(true);
							return;
						}

					}

					// get all the selected videos
					List<int> vidIDs = new List<int>();
					foreach (object obj in lbVideos.SelectedItems)
					{
						VideoLocalVM vid = obj as VideoLocalVM;
						vidIDs.Add(vid.VideoLocalID);
					}

					string msg = JMMServerVM.Instance.clientBinaryHTTP.AssociateMultipleFiles(vidIDs, series.AnimeSeriesID.Value, startEpNum, MultipleTypeSingle);
					if (msg.Length > 0)
					{
						MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					else
					{
						RefreshUnrecognisedFiles();

						//MainListHelperVM.Instance.UpdateHeirarchy(ep, ((MainWindow)parentWindow).epListMain);
						//ep.RefreshFilesForEpisode();
					}

				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
			EnableDisableControls(true);
		}

		void txtStartEpNum_TextChanged(object sender, TextChangedEventArgs e)
		{
			SetConfirmDetails();
		}

		private void CommandBinding_OpenFolder(object sender, ExecutedRoutedEventArgs e)
		{
			object obj = e.Parameter;
			if (obj == null) return;

			if (obj.GetType() == typeof(VideoLocalVM))
			{
				VideoLocalVM vid = obj as VideoLocalVM;

				if (File.Exists(vid.FullPath))
				{
					Utils.OpenFolderAndSelectFile(vid.FullPath);
				}
				else
				{
					MessageBox.Show(Properties.Resources.MSG_ERR_FileNotFound, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		private void CommandBinding_IgnoreFile(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				Window parentWindow = Window.GetWindow(this);

				object obj = e.Parameter;
				if (obj == null) return;

				if (obj.GetType() == typeof(VideoLocalVM))
				{
					VideoLocalVM vid = obj as VideoLocalVM;
					EnableDisableControls(false);

					string result = JMMServerVM.Instance.clientBinaryHTTP.SetIgnoreStatusOnFile(vid.VideoLocalID, true);
					if (result.Length > 0)
						MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					else
						RefreshUnrecognisedFiles();
					
				}
				if (obj.GetType() == typeof(MultipleVideos))
				{
					MultipleVideos mv = obj as MultipleVideos;
					foreach (int id in mv.VideoLocalIDs)
					{
						string result = JMMServerVM.Instance.clientBinaryHTTP.SetIgnoreStatusOnFile(id, true);
						if (result.Length > 0)
							MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					RefreshUnrecognisedFiles();
				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}

			EnableDisableControls(true);
		}

		private void CommandBinding_DeleteFile(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				Window parentWindow = Window.GetWindow(this);

				object obj = e.Parameter;
				if (obj == null) return;

				if (obj.GetType() == typeof(VideoLocalVM))
				{
					VideoLocalVM vid = obj as VideoLocalVM;

					MessageBoxResult res = MessageBox.Show(string.Format("Are you sure you want to delete this file: {0}", vid.FullPath),
					"Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
					if (res == MessageBoxResult.Yes)
					{
						string result = JMMServerVM.Instance.clientBinaryHTTP.DeleteVideoLocalAndFile(vid.VideoLocalID);
						if (result.Length > 0)
							MessageBox.Show(result, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
						else
							RefreshUnrecognisedFiles();
					}

				}
				
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		private void CommandBinding_RehashFile(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				Window parentWindow = Window.GetWindow(this);

				object obj = e.Parameter;
				if (obj == null) return;

				if (obj.GetType() == typeof(VideoLocalVM))
				{
					VideoLocalVM vid = obj as VideoLocalVM;
					EnableDisableControls(false);

					JMMServerVM.Instance.clientBinaryHTTP.RehashFile(vid.VideoLocalID);
				}
				if (obj.GetType() == typeof(MultipleVideos))
				{
					MultipleVideos mv = obj as MultipleVideos;
					foreach (int id in mv.VideoLocalIDs)
					{
						JMMServerVM.Instance.clientBinaryHTTP.RehashFile(id);
					}
				}

				MessageBox.Show(Properties.Resources.MSG_INFO_AddedQueueCmds, "Done", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}

			EnableDisableControls(true);
		}

		private void CommandBinding_RescanFile(object sender, ExecutedRoutedEventArgs e)
		{
			try
			{
				Window parentWindow = Window.GetWindow(this);

				object obj = e.Parameter;
				if (obj == null) return;

				if (obj.GetType() == typeof(VideoLocalVM))
				{
					VideoLocalVM vid = obj as VideoLocalVM;
					EnableDisableControls(false);

					JMMServerVM.Instance.clientBinaryHTTP.RescanFile(vid.VideoLocalID);
				}

				MessageBox.Show(Properties.Resources.MSG_INFO_AddedQueueCmds, "Done", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}

			EnableDisableControls(true);
		}


		void lbVideos_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				ccDetail.Content = null;
				ccDetailMultiple.Content = null;

				AnyVideosSelected = lbVideos.SelectedItems.Count > 0;
				OneVideoSelected = lbVideos.SelectedItems.Count == 1;
				MultipleVideosSelected = lbVideos.SelectedItems.Count > 1;

				MultipleTypeRange = cboMultiType.SelectedIndex == 0;
				MultipleTypeSingle = cboMultiType.SelectedIndex == 1;

				// if only one video selected
				if (OneVideoSelected)
				{
					VideoLocalVM vid = lbVideos.SelectedItem as VideoLocalVM;
					ccDetail.Content = vid;
				}

				// if only one video selected
				if (MultipleVideosSelected)
				{
					MultipleVideos mv = new MultipleVideos();
					mv.SelectedCount = lbVideos.SelectedItems.Count;
					mv.VideoLocalIDs = new List<int>();
					mv.VideoLocals = new List<VideoLocalVM>();

					foreach (object obj in lbVideos.SelectedItems)
					{
						VideoLocalVM vid = obj as VideoLocalVM;
						mv.VideoLocalIDs.Add(vid.VideoLocalID);
						mv.VideoLocals.Add(vid);
					}

					ccDetailMultiple.Content = mv;
				}

				SetConfirmDetails();
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		void cboEpisodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SetConfirmDetails();
		}

		void lbSeries_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (lbSeries.Items.Count == 0) return;
				if (lbSeries.SelectedItem == null) return;

				AnimeSeriesVM series = lbSeries.SelectedItem as AnimeSeriesVM;
				List<AnimeEpisodeVM> eps = series.AllEpisodes;

				List<SortPropOrFieldAndDirection> sortCriteria = new List<SortPropOrFieldAndDirection>();
				sortCriteria.Add(new SortPropOrFieldAndDirection("EpisodeType", false, SortType.eInteger));
				sortCriteria.Add(new SortPropOrFieldAndDirection("EpisodeNumber", false, SortType.eInteger));

				eps = Sorting.MultiSort<AnimeEpisodeVM>(eps, sortCriteria);

				cboEpisodes.ItemsSource = eps;
				if (cboEpisodes.Items.Count > 0)
					cboEpisodes.SelectedIndex = 0;

				SetConfirmDetails();
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		private void SetConfirmDetails()
		{
			try
			{
				MultipleTypeRange = cboMultiType.SelectedIndex == 0;
				MultipleTypeSingle = cboMultiType.SelectedIndex == 1;

				btnConfirm.Visibility = System.Windows.Visibility.Hidden;
				cboEpisodes.Visibility = System.Windows.Visibility.Visible;

				if (lbVideos.SelectedItems.Count == 0)
					btnConfirm.Visibility = System.Windows.Visibility.Hidden;

				// evaluate selected single file
				if (OneVideoSelected)
				{
					if (cboMultiType.SelectedIndex == 0)
					{
						// episode range
						int startEpNum = 0, endEpNum = 0;
						int.TryParse(txtStartEpNum.Text, out startEpNum);
						int.TryParse(txtEndEpNumSingle.Text, out endEpNum);

						if (startEpNum > 0 && endEpNum > 0 && endEpNum > startEpNum)
							btnConfirm.Visibility = System.Windows.Visibility.Visible;
					}
					else
					{
						// single episode
						if (lbVideos.SelectedItem != null && cboEpisodes.SelectedItem != null)
							btnConfirm.Visibility = System.Windows.Visibility.Visible;
					}
				}

				// evaluate multiple selected files
				if (MultipleVideosSelected)
				{
					if (cboMultiType.SelectedIndex == 0)
					{
						// episode range
						int startEpNum = 0;
						int.TryParse(txtStartEpNum.Text, out startEpNum);
						if (startEpNum > 0)
						{
							btnConfirm.Visibility = System.Windows.Visibility.Visible;
							int endEpNum = startEpNum + lbVideos.SelectedItems.Count - 1;
							txtEndEpNum.Text = endEpNum.ToString();
						}
					}
					else
					{
						// single episode
						if (cboEpisodes.SelectedItem != null)
							btnConfirm.Visibility = System.Windows.Visibility.Visible;
					}
				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}
		

		void txtSeriesSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			ViewSeries.Refresh();
		}

		void btnRefresh_Click(object sender, RoutedEventArgs e)
		{
			RefreshUnrecognisedFiles();
		}

		public void RefreshUnrecognisedFiles()
		{
			try
			{
				UnrecognisedFiles.Clear();
				if (!JMMServerVM.Instance.ServerOnline) return;

				List<JMMServerBinary.Contract_VideoLocal> vids = JMMServerVM.Instance.clientBinaryHTTP.GetUnrecognisedFiles(JMMServerVM.Instance.CurrentUser.JMMUserID.Value);
				FileCount = vids.Count;

				foreach (JMMServerBinary.Contract_VideoLocal vid in vids)
				{
					UnrecognisedFiles.Add(new VideoLocalVM(vid));
				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		public void RefreshSeries()
		{
			try
			{
				MainListHelperVM.Instance.AllAnimeDetailedDictionary = null;
				AllSeries.Clear();
				if (!JMMServerVM.Instance.ServerOnline) return;
				foreach (JMMServerBinary.Contract_AnimeSeries ser in JMMServerVM.Instance.clientBinaryHTTP.GetAllSeries(JMMServerVM.Instance.CurrentUser.JMMUserID.Value))
				{
					AllSeries.Add(new AnimeSeriesVM(ser));
				}
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		private bool SeriesSearchFilter(object obj)
		{
			AnimeSeriesVM servm = obj as AnimeSeriesVM;
			if (servm == null) return true;

			return GroupSearchFilterHelper.EvaluateSeriesTextSearch(servm, txtSeriesSearch.Text);
		}

		private bool FileSearchFilter(object obj)
		{
			VideoLocalVM vid = obj as VideoLocalVM;
			if (vid == null) return true;

			int index = vid.FilePath.IndexOf(txtFileSearch.Text.Trim(), 0, StringComparison.InvariantCultureIgnoreCase);
			if (index > -1) return true;
			return false;
		}
	}
}
