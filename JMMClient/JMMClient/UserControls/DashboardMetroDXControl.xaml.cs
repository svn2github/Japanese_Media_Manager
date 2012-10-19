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
using System.Windows.Shapes;
using DevExpress.Xpf.LayoutControl;
using System.ComponentModel;
using NLog;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core;
using System.Diagnostics;

namespace JMMClient.UserControls
{
	/// <summary>
	/// Interaction logic for DashboardMetroDXControl.xaml
	/// </summary>
	public partial class DashboardMetroDXControl : UserControl
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		BackgroundWorker refreshDataWorker = new BackgroundWorker();
		BackgroundWorker refreshContinueWatchingWorker = new BackgroundWorker();
		BackgroundWorker refreshRandomSeriesWorker = new BackgroundWorker();
		BackgroundWorker refreshActivityWorker = new BackgroundWorker();
		BackgroundWorker refreshNewEpisodesWorker = new BackgroundWorker();

		public ObservableCollection<MetroDashSection> Sections { get; set; }
		public ICollectionView ViewSections { get; set; }

		public static readonly DependencyProperty IsLoadingContinueWatchingProperty = DependencyProperty.Register("IsLoadingContinueWatching",
			typeof(bool), typeof(DashboardMetroDXControl), new UIPropertyMetadata(false, null));

		public bool IsLoadingContinueWatching
		{
			get { return (bool)GetValue(IsLoadingContinueWatchingProperty); }
			set { SetValue(IsLoadingContinueWatchingProperty, value); }
		}

		public static readonly DependencyProperty IsLoadingRandomSeriesProperty = DependencyProperty.Register("IsLoadingRandomSeries",
			typeof(bool), typeof(DashboardMetroDXControl), new UIPropertyMetadata(false, null));

		public bool IsLoadingRandomSeries
		{
			get { return (bool)GetValue(IsLoadingRandomSeriesProperty); }
			set { SetValue(IsLoadingRandomSeriesProperty, value); }
		}

		public static readonly DependencyProperty IsLoadingTraktActivityProperty = DependencyProperty.Register("IsLoadingTraktActivity",
			typeof(bool), typeof(DashboardMetroDXControl), new UIPropertyMetadata(false, null));

		public bool IsLoadingTraktActivity
		{
			get { return (bool)GetValue(IsLoadingTraktActivityProperty); }
			set { 
				SetValue(IsLoadingTraktActivityProperty, value);
				SetSectionVisibility(DashboardMetroProcessType.TraktActivity);
			}
		}

		public static readonly DependencyProperty IsLoadingNewEpisodesProperty = DependencyProperty.Register("IsLoadingNewEpisodes",
			typeof(bool), typeof(DashboardMetroDXControl), new UIPropertyMetadata(false, null));

		public static readonly DependencyProperty Dash_TraktActivity_ColumnProperty = DependencyProperty.Register("Dash_TraktActivity_Column",
			typeof(int), typeof(DashboardMetroDXControl), new UIPropertyMetadata((int)1, null));

		public int Dash_TraktActivity_Column
		{
			get { return (int)GetValue(Dash_TraktActivity_ColumnProperty); }
			set { SetValue(Dash_TraktActivity_ColumnProperty, value); }
		}

		public static readonly DependencyProperty Dash_TraktActivity_VisibilityProperty = DependencyProperty.Register("Dash_TraktActivity_Visibility",
			typeof(Visibility), typeof(DashboardMetroDXControl), new UIPropertyMetadata(Visibility.Visible, null));

		public Visibility Dash_TraktActivity_Visibility
		{
			get { return (Visibility)GetValue(Dash_TraktActivity_VisibilityProperty); }
			set { SetValue(Dash_TraktActivity_VisibilityProperty, value); }
		}



		public static readonly DependencyProperty Dash_ContinueWatching_ColumnProperty = DependencyProperty.Register("Dash_ContinueWatching_Column",
			typeof(int), typeof(DashboardMetroDXControl), new UIPropertyMetadata((int)2, null));

		public int Dash_ContinueWatching_Column
		{
			get { return (int)GetValue(Dash_ContinueWatching_ColumnProperty); }
			set { SetValue(Dash_ContinueWatching_ColumnProperty, value); }
		}

		public static readonly DependencyProperty Dash_ContinueWatching_VisibilityProperty = DependencyProperty.Register("Dash_ContinueWatching_Visibility",
			typeof(Visibility), typeof(DashboardMetroDXControl), new UIPropertyMetadata(Visibility.Visible, null));

		public Visibility Dash_ContinueWatching_Visibility
		{
			get { return (Visibility)GetValue(Dash_ContinueWatching_VisibilityProperty); }
			set { SetValue(Dash_ContinueWatching_VisibilityProperty, value); }
		}




		public static readonly DependencyProperty Dash_RandomSeries_ColumnProperty = DependencyProperty.Register("Dash_RandomSeries_Column",
			typeof(int), typeof(DashboardMetroDXControl), new UIPropertyMetadata((int)3, null));

		public int Dash_RandomSeries_Column
		{
			get { return (int)GetValue(Dash_RandomSeries_ColumnProperty); }
			set { SetValue(Dash_RandomSeries_ColumnProperty, value); }
		}

		public static readonly DependencyProperty Dash_RandomSeries_VisibilityProperty = DependencyProperty.Register("Dash_RandomSeries_Visibility",
			typeof(Visibility), typeof(DashboardMetroDXControl), new UIPropertyMetadata(Visibility.Visible, null));

		public Visibility Dash_RandomSeries_Visibility
		{
			get { return (Visibility)GetValue(Dash_RandomSeries_VisibilityProperty); }
			set { SetValue(Dash_RandomSeries_VisibilityProperty, value); }
		}



		public static readonly DependencyProperty Dash_NewEpisodes_ColumnProperty = DependencyProperty.Register("Dash_NewEpisodes_Column",
			typeof(int), typeof(DashboardMetroDXControl), new UIPropertyMetadata((int)4, null));

		public int Dash_NewEpisodes_Column
		{
			get { return (int)GetValue(Dash_NewEpisodes_ColumnProperty); }
			set { SetValue(Dash_NewEpisodes_ColumnProperty, value); }
		}

		public static readonly DependencyProperty Dash_NewEpisodes_VisibilityProperty = DependencyProperty.Register("Dash_NewEpisodes_Visibility",
			typeof(Visibility), typeof(DashboardMetroDXControl), new UIPropertyMetadata(Visibility.Visible, null));

		public Visibility Dash_NewEpisodes_Visibility
		{
			get { return (Visibility)GetValue(Dash_NewEpisodes_VisibilityProperty); }
			set { SetValue(Dash_NewEpisodes_VisibilityProperty, value); }
		}


		public bool IsLoadingNewEpisodes
		{
			get { return (bool)GetValue(IsLoadingNewEpisodesProperty); }
			set { SetValue(IsLoadingNewEpisodesProperty, value); }
		}

		public DashboardMetroDXControl()
		{
			InitializeComponent();

			refreshDataWorker.DoWork += new DoWorkEventHandler(refreshDataWorker_DoWork);
			refreshDataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshDataWorker_RunWorkerCompleted);

			refreshContinueWatchingWorker.DoWork += new DoWorkEventHandler(refreshContinueWatchingWorker_DoWork);
			refreshContinueWatchingWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshContinueWatchingWorker_RunWorkerCompleted);

			refreshRandomSeriesWorker.DoWork += new DoWorkEventHandler(refreshRandomSeriesWorker_DoWork);
			refreshRandomSeriesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshRandomSeriesWorker_RunWorkerCompleted);

			refreshActivityWorker.DoWork += new DoWorkEventHandler(refreshActivityWorker_DoWork);
			refreshActivityWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshActivityWorker_RunWorkerCompleted);

			refreshNewEpisodesWorker.DoWork += new DoWorkEventHandler(refreshNewEpisodesWorker_DoWork);
			refreshNewEpisodesWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(refreshNewEpisodesWorker_RunWorkerCompleted);

			this.Loaded += new RoutedEventHandler(DashboardMetroDXControl_Loaded);
			btnToggleDash.Click += new RoutedEventHandler(btnToggleDash_Click);

			btnContinueWatchingIncrease.Click += new RoutedEventHandler(btnContinueWatchingIncrease_Click);
			btnContinueWatchingReduce.Click += new RoutedEventHandler(btnContinueWatchingReduce_Click);

			btnRefresh.Click += new RoutedEventHandler(btnRefresh_Click);
			btnRefreshRandomSeries.Click += new RoutedEventHandler(btnRefreshRandomSeries_Click);
			btnRefreshActivity.Click += new RoutedEventHandler(btnRefreshActivity_Click);
			btnRefreshNewEpisodes.Click += new RoutedEventHandler(btnRefreshNewEpisodes_Click);

			btnOptions.Click += new RoutedEventHandler(btnOptions_Click);

			cboImageType.Items.Clear();
			cboImageType.Items.Add("Fanart");
			cboImageType.Items.Add("Posters");

			if (AppSettings.DashMetroImageType == DashboardMetroImageType.Fanart)
				cboImageType.SelectedIndex = 0;
			else
				cboImageType.SelectedIndex = 1;

			cboImageType.SelectionChanged += new SelectionChangedEventHandler(cboImageType_SelectionChanged);

			DashboardMetroVM.Instance.OnFinishedProcessEvent += new DashboardMetroVM.FinishedProcessHandler(Instance_OnFinishedProcessEvent);

			Sections = new ObservableCollection<MetroDashSection>();
			ViewSections = CollectionViewSource.GetDefaultView(Sections);

			SetSectionOrder();

			LayoutRoot.PreviewMouseWheel += new MouseWheelEventHandler(LayoutRoot_PreviewMouseWheel);
			ScrollerDashMetroDX.PreviewMouseWheel += new MouseWheelEventHandler(ScrollerDashMetroDX_PreviewMouseWheel);
		}

		void ScrollerDashMetroDX_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			try
			{
				if (e.Delta > 0)
					ScrollerDashMetroDX.LineLeft();
				else
					ScrollerDashMetroDX.LineRight();
				e.Handled = true;
			}
			catch { }
		}

		void LayoutRoot_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			try
			{
				if (e.Delta > 0)
					ScrollerDashMetroDX.LineLeft();
				else
					ScrollerDashMetroDX.LineRight();
				e.Handled = true;
			}
			catch { }
		}

		private void SetSectionOrder()
		{
			int posTrakt = 0, posCont = 0, posRSeries = 0, posNewEps = 0;
			Visibility visTrakt = System.Windows.Visibility.Visible,
				visCont = System.Windows.Visibility.Visible,
				visRSeries = System.Windows.Visibility.Visible,
				visNewEps = System.Windows.Visibility.Visible;

			UserSettingsVM.Instance.GetDashboardMetroSectionPosition(DashboardMetroProcessType.TraktActivity, ref posTrakt, ref visTrakt);
			UserSettingsVM.Instance.GetDashboardMetroSectionPosition(DashboardMetroProcessType.ContinueWatching, ref posCont, ref visCont);
			UserSettingsVM.Instance.GetDashboardMetroSectionPosition(DashboardMetroProcessType.RandomSeries, ref posRSeries, ref visRSeries);
			UserSettingsVM.Instance.GetDashboardMetroSectionPosition(DashboardMetroProcessType.NewEpisodes, ref posNewEps, ref visNewEps);

			Dash_TraktActivity_Column = posTrakt;
			Dash_ContinueWatching_Column = posCont;
			Dash_RandomSeries_Column = posRSeries;
			Dash_NewEpisodes_Column = posNewEps;

			Dash_TraktActivity_Visibility = visTrakt;
			Dash_ContinueWatching_Visibility = visCont;
			Dash_RandomSeries_Visibility = visRSeries;
			Dash_NewEpisodes_Visibility = visNewEps;

			Sections.Clear();
			List<MetroDashSection> sections = UserSettingsVM.Instance.GetMetroDashSections();
			foreach (MetroDashSection sect in sections)
				Sections.Add(sect);

			ViewSections.Refresh();
		}

		private void CommandBinding_MoveUpSection(object sender, ExecutedRoutedEventArgs e)
		{
			object obj = e.Parameter;
			if (obj == null) return;

			MetroDashSection sect = obj as MetroDashSection;
			if (sect == null) return;

			UserSettingsVM.Instance.MoveUpDashboardMetroSection(sect.SectionType);
			SetSectionOrder();
		}

		private void CommandBinding_MoveDownSection(object sender, ExecutedRoutedEventArgs e)
		{
			object obj = e.Parameter;
			if (obj == null) return;

			MetroDashSection sect = obj as MetroDashSection;
			if (sect == null) return;

			UserSettingsVM.Instance.MoveDownDashboardMetroSection(sect.SectionType);
			SetSectionOrder();
		}

		private void CommandBinding_EnableSectionCommand(object sender, ExecutedRoutedEventArgs e)
		{
			object obj = e.Parameter;
			if (obj == null) return;

			MetroDashSection sect = obj as MetroDashSection;
			if (sect == null) return;

			UserSettingsVM.Instance.EnableDisableDashboardMetroSection(sect.SectionType, true);
			SetSectionOrder();
		}

		private void CommandBinding_DisableSectionCommand(object sender, ExecutedRoutedEventArgs e)
		{
			object obj = e.Parameter;
			if (obj == null) return;

			MetroDashSection sect = obj as MetroDashSection;
			if (sect == null) return;

			UserSettingsVM.Instance.EnableDisableDashboardMetroSection(sect.SectionType, false);
			SetSectionOrder();
		}

		private void SetSectionVisibility(DashboardMetroProcessType sectionType)
		{
			/*switch (sectionType)
			{
				case DashboardMetroProcessType.ContinueWatching: break;

				case DashboardMetroProcessType.NewEpisodes: break;
				case DashboardMetroProcessType.RandomSeries: break;
				case DashboardMetroProcessType.TraktActivity:
					if (IsLoadingTraktActivity) Dash_TraktActivity_Visibility = System.Windows.Visibility.Collapsed;
					else
					{
						Dash_TraktActivity_Visibility = System.Windows.Visibility.Visible;
					}
					break;
			}*/
		}
		

		

		

		

		void cboImageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cboImageType.SelectedIndex == 0)
				AppSettings.DashMetroImageType = DashboardMetroImageType.Fanart;
			else
				AppSettings.DashMetroImageType = DashboardMetroImageType.Posters;

			UserSettingsVM.Instance.DashMetro_Image_Height = UserSettingsVM.Instance.DashMetro_Image_Height;
			RefreshAllData();
		}

		

		

		void btnOptions_Click(object sender, RoutedEventArgs e)
		{
			DashboardMetroVM.Instance.IsBeingEdited = !DashboardMetroVM.Instance.IsBeingEdited;
		}

		void btnRefresh_Click(object sender, RoutedEventArgs e)
		{
			IsLoadingContinueWatching = true;
			refreshContinueWatchingWorker.RunWorkerAsync(false);
		}

		void btnRefreshRandomSeries_Click(object sender, RoutedEventArgs e)
		{
			IsLoadingRandomSeries = true;
			refreshRandomSeriesWorker.RunWorkerAsync(false);
		}

		void btnRefreshNewEpisodes_Click(object sender, RoutedEventArgs e)
		{
			IsLoadingNewEpisodes = true;
			refreshNewEpisodesWorker.RunWorkerAsync(false);
		}

		void btnRefreshActivity_Click(object sender, RoutedEventArgs e)
		{
			FrameworkElements obj = tileLayoutActivity.GetChildren(false);
			IsLoadingTraktActivity = true;
			refreshActivityWorker.RunWorkerAsync(false);
		}

		void btnContinueWatchingReduce_Click(object sender, RoutedEventArgs e)
		{
			UserSettingsVM.Instance.DashMetro_Image_Height = UserSettingsVM.Instance.DashMetro_Image_Height - 7;
		}

		void btnContinueWatchingIncrease_Click(object sender, RoutedEventArgs e)
		{
			UserSettingsVM.Instance.DashMetro_Image_Height = UserSettingsVM.Instance.DashMetro_Image_Height + 7;
		}

		void btnToggleDash_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainwdw = (MainWindow)Window.GetWindow(this);
			mainwdw.ShowDashMetroView(MetroViews.MainNormal);
		}

		void DashboardMetroDXControl_Loaded(object sender, RoutedEventArgs e)
		{
			MainWindow mainwdw = (MainWindow)Window.GetWindow(this);
			DashboardMetroVM.Instance.InitNavigator(mainwdw);
		}

		public void RefreshAllData()
		{
			RefreshDashOptions opt = new RefreshDashOptions()
			{
				TraktActivity = Dash_TraktActivity_Visibility == System.Windows.Visibility.Visible,
				ContinueWatching = Dash_ContinueWatching_Visibility == System.Windows.Visibility.Visible,
				RandomSeries = Dash_RandomSeries_Visibility == System.Windows.Visibility.Visible,
				NewEpisodes = Dash_NewEpisodes_Visibility == System.Windows.Visibility.Visible
			};

			if (opt.ContinueWatching) IsLoadingContinueWatching = true;
			if (opt.RandomSeries) IsLoadingRandomSeries = true;
			if (opt.TraktActivity) IsLoadingTraktActivity = true;
			if (opt.NewEpisodes) IsLoadingNewEpisodes = true;

			refreshDataWorker.RunWorkerAsync(opt);
		}

		void refreshDataWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//bool refreshAll = (bool)e.Result;
			//IsLoadingContinueWatching = false;
			

			//if (refreshAll)
			//	refreshRandomSeriesWorker.RunWorkerAsync(true);
		}

		void refreshDataWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				RefreshDashOptions opt = e.Argument as RefreshDashOptions;

				DashboardMetroVM.Instance.RefreshAllData(
					opt.TraktActivity,
					opt.ContinueWatching,
					opt.RandomSeries,
					opt.NewEpisodes);
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		void Instance_OnFinishedProcessEvent(FinishedProcessEventArgs ev)
		{
			System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate()
			{
				switch (ev.ProcessType)
				{
					case DashboardMetroProcessType.ContinueWatching: IsLoadingContinueWatching = false; break;
					case DashboardMetroProcessType.RandomSeries: IsLoadingRandomSeries = false; break;
					case DashboardMetroProcessType.TraktActivity: IsLoadingTraktActivity = false; break;
					case DashboardMetroProcessType.NewEpisodes: IsLoadingNewEpisodes = false; break;
				}
			});
		}

		void refreshContinueWatchingWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			
		}

		void refreshContinueWatchingWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				DashboardMetroVM.Instance.RefreshContinueWatching();
			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		void refreshRandomSeriesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//bool refreshAll = (bool)e.Result;
			//IsLoadingRandomSeries = false;

			//if (refreshAll)
			//	refreshActivityWorker.RunWorkerAsync(true);
		}

		void refreshRandomSeriesWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			//bool refreshAll = (bool)e.Argument;
			DashboardMetroVM.Instance.RefreshRandomSeries();
			//e.Result = refreshAll;
		}

		void refreshNewEpisodesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		void refreshNewEpisodesWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			DashboardMetroVM.Instance.RefreshNewEpisodes();
		}

		void refreshActivityWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
		}

		void refreshActivityWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			//bool refreshAll = (bool)e.Argument;
			DashboardMetroVM.Instance.RefreshTraktActivity();
			//e.Result = refreshAll;
		}

		private void tileLayoutControl1_DragOver(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.Move;
			e.Handled = true;
		}

		private void tileLayoutControl1_Drop(object sender, DragEventArgs e)
		{
			/*

			SomeItem item = (SomeItem)e.Data.GetData(typeof(SomeItem));
			TileLayoutControl tileLayoutControl = (TileLayoutControl)sender;
			((ObservableCollection<SomeItem>)tileLayoutControl.ItemsSource).Add(item);*/
		}

		private void tileLayoutTraktActivity_TileClick(object sender, TileClickEventArgs e)
		{
			try
			{
				Tile mytile = e.Tile;
				object item = mytile.DataContext as object;
				if (item == null) return;



				if (item.GetType() == typeof(TraktActivityTile))
				{
					TraktActivityTile tile = item as TraktActivityTile;
					Uri uri = new Uri(tile.URL);
					Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
				}

				if (item.GetType() == typeof(TraktShoutTile))
				{
					TraktShoutTile tile = item as TraktShoutTile;
					Uri uri = new Uri(tile.URL);
					Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
				}

				if (item.GetType() == typeof(Trakt_SignupVM))
				{
					MainWindow mainwdw = (MainWindow)Window.GetWindow(this);
					mainwdw.tabControl1.SelectedIndex = MainWindow.TAB_MAIN_Settings;
					mainwdw.tabSettingsChild.SelectedIndex = MainWindow.TAB_Settings_TvDB;
				}


			}
			catch (Exception ex)
			{
				Utils.ShowErrorMessage(ex);
			}
		}

		private void tileLayoutContinueWatching_TileClick(object sender, TileClickEventArgs e)
		{
			Tile mytile = e.Tile;
			ContinueWatchingTile item = mytile.DataContext as ContinueWatchingTile;
			if (item == null) return;

			DashboardMetroVM.Instance.NavigateForward(MetroViews.ContinueWatching, item.AnimeSeries);
		}

		private void tileLayoutRandomSeries_TileClick(object sender, TileClickEventArgs e)
		{
			Tile mytile = e.Tile;
			RandomSeriesTile item = mytile.DataContext as RandomSeriesTile;

			DashboardMetroVM.Instance.NavigateForward(MetroViews.ContinueWatching, item.AnimeSeries);
		}

		private void tileLayoutNewEpisodes_TileClick(object sender, TileClickEventArgs e)
		{
			Tile mytile = e.Tile;
			NewEpisodeTile item = mytile.DataContext as NewEpisodeTile;

			DashboardMetroVM.Instance.NavigateForward(MetroViews.ContinueWatching, item.AnimeSeries);
		}
	}

	public class RefreshDashOptions
	{
		public bool TraktActivity { get; set; }
		public bool ContinueWatching { get; set; }
		public bool RandomSeries { get; set; }
		public bool NewEpisodes { get; set; }
	}
}
