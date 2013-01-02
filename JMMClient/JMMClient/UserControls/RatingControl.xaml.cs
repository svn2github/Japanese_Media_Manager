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
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace JMMClient.UserControls
{
	/// <summary>
	/// Interaction logic for RatingControl.xaml
	/// </summary>
	public partial class RatingControl : UserControl
	{

		public static readonly DependencyProperty RatingValueProperty =
		   DependencyProperty.Register("RatingValue", typeof(double), typeof(RatingControl),
									   new FrameworkPropertyMetadata(0.0,
																	 FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
																	 RatingValueChanged));
		private double _maxValue = 10;

		public delegate void RatingValueChangedHandler(RatingValueEventArgs ev);
		public event RatingValueChangedHandler OnRatingValueChangedEvent;
		protected void OnRatingValueChanged(RatingValueEventArgs ev)
		{
			if (OnRatingValueChangedEvent != null)
			{
				OnRatingValueChangedEvent(ev);
			}
		}

		public RatingControl()
		{
			InitializeComponent();
		}

		public double RatingValue
		{
			get { return (double)GetValue(RatingValueProperty); }
			set
			{
				if (value < 0)
				{
					SetValue(RatingValueProperty, 0);
				}
				else if (value > _maxValue)
				{
					SetValue(RatingValueProperty, _maxValue);
				}
				else
				{
					SetValue(RatingValueProperty, value);
				}


			}
		}

		private static void RatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			RatingControl parent = sender as RatingControl;

			NumberStyles style = NumberStyles.Number;
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

			double ratingValue = -1;
			double.TryParse(e.NewValue.ToString(), style, culture, out ratingValue);

			int numberOfButtonsToHighlight = (int)(2 * ratingValue);

			UIElementCollection children = ((StackPanel)(parent.Content)).Children;
			ToggleButton button = null;

			for (int i = 0; i < numberOfButtonsToHighlight; i++)
			{
				button = children[i] as ToggleButton;
				if (button != null)
					button.IsChecked = true;
			}

			for (int i = numberOfButtonsToHighlight; i < children.Count; i++)
			{
				button = children[i] as ToggleButton;
				if (button != null)
					button.IsChecked = false;
			}

		}

		private void RatingButtonClickEventHandler(Object sender, RoutedEventArgs e)
		{
			ToggleButton button = sender as ToggleButton;

			NumberStyles style = NumberStyles.Number;
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

			double newRating = -1;
			double.TryParse((String)button.Tag, style, culture, out newRating);

			if (RatingValue == newRating && newRating == 0.5)
			{
				RatingValue = 0.0;
			}
			else
			{
				RatingValue = newRating;
			}
			e.Handled = true;

			OnRatingValueChanged(new RatingValueEventArgs(RatingValue));
		}

		private void RatingButtonMouseEnterEventHandler(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ToggleButton button = sender as ToggleButton;

			NumberStyles style = NumberStyles.Number;
			CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

			double hoverRating = -1;
			double.TryParse((String)button.Tag, style, culture, out hoverRating);

			int numberOfButtonsToHighlight = (int)(2 * hoverRating);

			UIElementCollection children = RatingContentPanel.Children;

			ToggleButton hlbutton = null;

			for (int i = 0; i < numberOfButtonsToHighlight; i++)
			{
				hlbutton = children[i] as ToggleButton;
				if (hlbutton != null)
					hlbutton.IsChecked = true;
			}

			for (int i = numberOfButtonsToHighlight; i < children.Count; i++)
			{
				hlbutton = children[i] as ToggleButton;
				if (hlbutton != null)
					hlbutton.IsChecked = false;
			}
		}

		private void RatingButtonMouseLeaveEventHandler(object sender, System.Windows.Input.MouseEventArgs e)
		{
			double ratingValue = RatingValue;
			int numberOfButtonsToHighlight = (int)(2 * ratingValue);

			UIElementCollection children = RatingContentPanel.Children;
			ToggleButton button = null;

			for (int i = 0; i < numberOfButtonsToHighlight; i++)
			{
				button = children[i] as ToggleButton;
				if (button != null)
					button.IsChecked = true;
			}

			for (int i = numberOfButtonsToHighlight; i < children.Count; i++)
			{
				button = children[i] as ToggleButton;
				if (button != null)
					button.IsChecked = false;
			}
		}
	}

	public class RatingValueEventArgs : EventArgs
	{
		public readonly double RatingValue;

		public RatingValueEventArgs(double ratingValue)
		{
			RatingValue = ratingValue;
		}
	}
}
