using System.IO;
using System.Windows;
using System.Windows.Input;

using NAudio.Wave;
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

		//private Timer timer;
		private bool isSeekingManually = false;
		private bool isUserChangingSlider = false;
		private readonly MemoryStream stream;
		private readonly Mp3FileReader mp3Reader;
		private WaveOutEvent waveOut;
		private System.Windows.Threading.DispatcherTimer positionTimer;

		public MediaPlayerView( byte [ ] _stream, string _file )
		{
			InitializeComponent();

			timer = new()
			{
				Interval = TimeSpan.FromSeconds( .5 )
			};
			timer.Tick += Timer_Tick;

			tbFileName.Text = _file;

			stream = new( _stream );
			mp3Reader = new( stream );
			waveOut = new();
			waveOut.Init( mp3Reader );

			positionTimer = new System.Windows.Threading.DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds( 500 )
			};
			positionTimer.Tick += PositionTimer_Tick;

			if ( mp3Reader != null )
			{
				//duration = MP3MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
				duration = mp3Reader.TotalTime.TotalSeconds;
				TimeSpan timeSpan = TimeSpan.FromSeconds(duration);

				timelineSlider.Maximum = duration;
				tbTotalTime.Text = string.Format( "Totale tijdsduur: {0:mm\\:ss}", timeSpan );
			}
		}

		#region Stop playing
		private void btnStopClick( object sender, RoutedEventArgs e )
		{
			waveOut.Stop();
			timer.Stop();
			positionTimer.Stop();
		}
		#endregion

		#region Pause playing
		private void btnPauseClick( object sender, RoutedEventArgs e )
		{
			waveOut.Pause();
			timer.Stop();
		}
		#endregion

		#region Start/Resume playing
		private void btnPlayClick( object sender, RoutedEventArgs e )
		{
			timelineSlider.Maximum = mp3Reader.TotalTime.TotalSeconds;
			waveOut.Play();
			timer.Start();
			waveOut.Volume = ( float ) volumeSlider.Value;
			positionTimer.Start();
		}
		#endregion

		#region Change the volume of the playback
		private void ChangeMediaVolume( object sender, RoutedPropertyChangedEventArgs<double> args )
		{
			// When the wMemoryStream is not yet initialized (waveOut = null) then volume change cannot be executed
			if ( waveOut != null )
			{
				float volume = ( float ) volumeSlider.Value;
				waveOut.Volume = volume;
			}
		}
		#endregion

		// When the media opens, initialize the "Seek To" slider maximum value
		// to the total number of seconds in the length of the media clip.
		private void Element_MediaOpened( object sender, EventArgs e )
		{
			if ( MP3MediaElement.NaturalDuration.HasTimeSpan )
			{
				timelineSlider.Visibility = Visibility.Visible;
				tbDuration.Visibility = Visibility.Visible;
				tbTotalTime.Visibility = Visibility.Visible;

				duration = MP3MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
				TimeSpan timeSpan = TimeSpan.FromSeconds(duration);

				timelineSlider.Maximum = duration;
				tbTotalTime.Text = string.Format( "Tijdsduur: {0:mm\\:ss}", timeSpan );
				timer.Start();
			}
			else
			{
				timelineSlider.Visibility = Visibility.Collapsed;
				tbDuration.Visibility = Visibility.Collapsed;
				tbTotalTime.Visibility = Visibility.Collapsed;
			}
		}

		// When the media playback is finished. Stop() the media to seek to media start.
		private void Element_MediaEnded( object sender, EventArgs e )
		{
			waveOut.Stop();
			timelineSlider.Value = 0;
		}

		#region TimeSlider lets user Jump to different parts of the media (seek to).
		private void SeekToMediaPosition( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			if ( waveOut != null && mp3Reader != null )
			{
				// Bereken de nieuwe positie op basis van de waarde van de tijdschuifregelaar
				long newPosition = (long)(e.NewValue * mp3Reader.Length);

				// Stel de nieuwe positie in op de Mp3FileReader
				mp3Reader.Position = newPosition;
			}
		}
		#endregion

		#region Actual position for the TimeSlider
		private void PositionTimer_Tick( object sender, EventArgs e )
		{
			if ( waveOut != null && mp3Reader != null )
			{
				timelineSlider.Value = mp3Reader.CurrentTime.TotalSeconds;
				tbDuration.Text = $"({mp3Reader.CurrentTime.ToString( "mm\\:ss" )})";
			}
		}
		#endregion

		private void timelineSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			if ( isUserChangingSlider )
			{           // Markeer dat de schuifregelaar handmatig wordt bijgewerkt
				isSeekingManually = true;

				// Hier kun je verdere logica uitvoeren als dat nodig is bij het handmatig bijwerken van de schuifregelaar

				// Reset de vlag na het handmatig bijwerken van de schuifregelaar
				isSeekingManually = false;
			}
		}

		private void Window_Closing( object sender, System.ComponentModel.CancelEventArgs e )
		{
			waveOut.Stop();
			timer.Stop();
			mp3Reader.Dispose();
			waveOut.Dispose();
			stream.Dispose();
			base.OnClosed( e );
		}

		private void timelineSlider_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
		{
			isUserChangingSlider = true;
			//timelineSlider.Value = mp3Reader.CurrentTime.TotalSeconds;
			//tbDuration.Text = $"({mp3Reader.CurrentTime.ToString( "mm\\:ss" )})";
			//mp3Reader.Position = ( long ) timelineSlider.Value;
		}

		private void timelineSlider_PreviewMouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			isUserChangingSlider = false;
		}

		private void Timer_Tick( object sender, EventArgs e )
		{
			//timelineSlider.Value = mp3Reader.Position;
			//remaining = TimeSpan.FromSeconds( duration - mp3Reader.Position );
			//tbDuration.Text = string.Format( "({0:mm\\:ss})", mp3Reader.CurrentTime.TotalSeconds );
			//tbDuration.Text = $"({mp3Reader.CurrentTime.ToString("mm\\:ss")})";
		}
	}
}
