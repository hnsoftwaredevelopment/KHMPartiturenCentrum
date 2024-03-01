using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHM.Converters
	{
	public class VolumeConverter : IValueConverter
		{
		public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
			{
			double volume = (double)value;
			int multipliedVolume = (int)(volume * 10);
			return multipliedVolume.ToString ();
			}

		public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
			{
			throw new NotImplementedException ();
			}
		}
	}
