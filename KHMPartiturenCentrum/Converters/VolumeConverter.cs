namespace KHM.Converters
{
	public class VolumeConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			double volume = (double)value;
			int multipliedVolume = (int)(volume);
			return ( ( int ) ( volume ) ).ToString();
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotImplementedException();
		}
	}
}
