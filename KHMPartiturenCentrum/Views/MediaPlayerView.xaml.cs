namespace KHM.Views
{
	/// <summary>
	/// Interaction logic for MediaPlayer.xaml
	/// </summary>
	public partial class MediaPlayerView : Window
	{
		private DispatcherTimer timer;
		private Double duration;
		private TimeSpan remaining;
		private string volume = "5";

		public MediaPlayerView( string _filePath, string _score, string _title, string _type, string _voice )
		{
			var FileType="";

			InitializeComponent();

			// Set the correct file ico display
			if ( _voice == "" )
			{
				// Instrumental file
				voiceicon.Visibility = Visibility.Collapsed;
				instrumentalicon.Visibility = Visibility.Visible;
			}
			else
			{
				// Voice file
				voiceicon.Visibility = Visibility.Visible;
				instrumentalicon.Visibility = Visibility.Collapsed;
			}

			// Set the ScoreNumber
			tbScore.Text = _score;

			// Set the tile
			tbTitle.Text = _title;

			// Set the file type
			switch ( _type.ToLower() )
			{
				case "b1":
					FileType = "Baritons";
					break;
				case "b2":
					FileType = "Bassen";
					break;
				case "t1":
					FileType = "1e Tenoren";
					break;
				case "t2":
					FileType = "2e Tenoren";
					break;
				case "pia":
					FileType = "Piano";
					break;
				case "uitv":
					FileType = "Uitvoering";
					break;
				case "tot":
					FileType = "Totaal";
					break;
			}

			tbPart.Text = FileType;

			timer = new()
			{
				Interval = TimeSpan.FromSeconds( 1 )
			};
			timer.Tick += Timer_Tick;

			MP3MediaElement.Source = new Uri( _filePath );
		}

		#region Stop playing
		private void btnStopClick( object sender, RoutedEventArgs e )
		{
			MP3MediaElement.Stop();
		}
		#endregion

		#region Pause playing
		private void btnPauseClick( object sender, RoutedEventArgs e )
		{
			MP3MediaElement.Pause();
		}
		#endregion

		#region Start/Resume playing
		private void btnPlayClick( object sender, RoutedEventArgs e )
		{
			MP3MediaElement.Play();
			MP3MediaElement.Volume = ( double ) volumeSlider.Value;
		}
		#endregion

		#region Change the volume of the playback
		private void ChangeMediaVolume( object sender, RoutedPropertyChangedEventArgs<double> args )
		{
			MP3MediaElement.Volume = ( double ) volumeSlider.Value / 100;
		}
		#endregion

		// When the media opens, initialize the "Seek To" slider maximum value
		// to the total number of seconds in the length of the media clip.
		private void Element_MediaOpened( object sender, EventArgs e )
		{
			if ( MP3MediaElement.NaturalDuration.HasTimeSpan )
			{
				tbDuration.Visibility = Visibility.Visible;
				tbTotalTime.Visibility = Visibility.Visible;

				duration = MP3MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
				TimeSpan timeSpan = TimeSpan.FromSeconds(duration);

				tbTotalTime.Text = string.Format( "Tijdsduur: {0:mm\\:ss}", timeSpan );
				timer.Start();
			}
			else
			{
				tbDuration.Visibility = Visibility.Collapsed;
				tbTotalTime.Visibility = Visibility.Collapsed;
			}
		}

		// When the media playback is finished. Stop() the media to seek to media start.
		private void Element_MediaEnded( object sender, EventArgs e )
		{
			MP3MediaElement.Stop();
			timelineSlider.Value = 0;
		}

		#region TimeSlider lets user Jump to different parts of the media (seek to).
		private void SeekToMediaPosition( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			MP3MediaElement.Position = TimeSpan.FromSeconds( timelineSlider.Value );
		}
		#endregion

		private void Window_Closing( object sender, System.ComponentModel.CancelEventArgs e )
		{
			MP3MediaElement.Source = null;
			timer.Stop();
			MP3MediaElement.Close();
		}

		private void Timer_Tick( object sender, EventArgs e )
		{
			if ( MP3MediaElement.NaturalDuration.HasTimeSpan )
			{
				timelineSlider.Value = MP3MediaElement.Position.TotalSeconds;
				remaining = TimeSpan.FromSeconds( duration - MP3MediaElement.Position.TotalSeconds );
				tbDuration.Text = string.Format( "({0:mm\\:ss})", remaining );
			}
		}

		private void volumeSlider_DrawLabel( object sender, Syncfusion.Windows.Controls.Navigation.DrawLabelEventArgs e )
		{
			e.Handled = true;
			e.FontFamily = new( "Georgia" );
			e.Foreground = Brushes.DarkSlateBlue;
		}
	}
}
