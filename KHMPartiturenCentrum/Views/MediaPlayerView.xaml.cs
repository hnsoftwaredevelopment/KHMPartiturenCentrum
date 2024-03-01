using System.IO;
using System.Windows;

using NAudio.Wave;

namespace KHM.Views;
/// <summary>
/// Interaction logic for MediaPlayer.xaml
/// </summary>
public partial class MediaPlayerView : Window
{
    private DispatcherTimer timer;
    private Double duration;
    private TimeSpan remaining;
    private string volume = "5";

    public MediaPlayerView ( byte[] _stream, string _file )
    {
        InitializeComponent ( );

        timer = new ( )
        {
            Interval = TimeSpan.FromSeconds ( 1 )
        };
        timer.Tick += Timer_Tick;

        tbFileName.Text = _file;

        using (MemoryStream stream = new ( _stream ) )
            {
            using (Mp3FileReader mp3Reader = new(stream))
                {
                    using (WaveOutEvent waveOut = new()) 
                    {
                        waveOut.Init(mp3Reader);

                    waveOut.Play();

                    while(waveOut.PlaybackState==PlaybackState.Playing)
                        {
                        System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        
        //var test = _stream;
        //MP3MediaElement.Source= new MemoryStream (_stream);
        //MP3MediaElement.Source = new Uri ( _stream );
    }

    private void btnStopClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Stop ( );
    }

    private void btnPauseClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Pause ( );
    }

    private void btnPlayClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Play ( );
        MP3MediaElement.Volume = ( double ) volumeSlider.Value;
    }

    // Change the volume of the media.
    private void ChangeMediaVolume ( object sender, RoutedPropertyChangedEventArgs<double> args )
    {
        MP3MediaElement.Volume = ( double ) volumeSlider.Value;
    }

    // When the media opens, initialize the "Seek To" slider maximum value
    // to the total number of seconds in the length of the media clip.
    private void Element_MediaOpened ( object sender, EventArgs e )
    {
        if ( MP3MediaElement.NaturalDuration.HasTimeSpan )
        {
            timelineSlider.Visibility = Visibility.Visible;
            tbDuration.Visibility = Visibility.Visible;
            tbTotalTime.Visibility = Visibility.Visible;

            duration = MP3MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            TimeSpan timeSpan = TimeSpan.FromSeconds(duration);

            timelineSlider.Maximum = duration;
            tbTotalTime.Text = string.Format ( "Tijdsduur: {0:mm\\:ss}", timeSpan );
            timer.Start ( );
        }
        else
        {
            timelineSlider.Visibility = Visibility.Collapsed;
            tbDuration.Visibility = Visibility.Collapsed;
            tbTotalTime.Visibility = Visibility.Collapsed;
        }
    }

    // When the media playback is finished. Stop() the media to seek to media start.
    private void Element_MediaEnded ( object sender, EventArgs e )
    {
        MP3MediaElement.Stop ( );
        timelineSlider.Value = 0;
    }

    // Jump to different parts of the media (seek to).
    private void SeekToMediaPosition ( object sender, RoutedPropertyChangedEventArgs<double> args )
    {
        MP3MediaElement.Position = TimeSpan.FromSeconds ( timelineSlider.Value );
    }

    private void Window_Closing ( object sender, System.ComponentModel.CancelEventArgs e )
    {
        MP3MediaElement.Source = null;
        timer.Stop ( );
        MP3MediaElement.Close ( );
    }

    private void Timer_Tick ( object sender, EventArgs e )
    {
        if ( MP3MediaElement.NaturalDuration.HasTimeSpan )
        {
            timelineSlider.Value = MP3MediaElement.Position.TotalSeconds;
            remaining = TimeSpan.FromSeconds ( duration - MP3MediaElement.Position.TotalSeconds );
            tbDuration.Text = string.Format ( "({0:mm\\:ss})", remaining );
        }
    }
}
