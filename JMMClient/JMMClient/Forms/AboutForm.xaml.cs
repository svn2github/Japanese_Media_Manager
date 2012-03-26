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
using System.Windows.Shapes;

namespace JMMClient.Forms
{
	/// <summary>
	/// Interaction logic for AboutForm.xaml
	/// </summary>
	public partial class AboutForm : Window
	{
		public AboutForm()
		{
			InitializeComponent();

			btnUpdates.Click += new RoutedEventHandler(btnUpdates_Click);
		}

		void btnUpdates_Click(object sender, RoutedEventArgs e)
		{
			automaticUpdater.ForceCheckForUpdate(true);
		}
	}
}
