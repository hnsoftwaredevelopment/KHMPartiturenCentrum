using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace KHM.Helpers
{
	public class IconTemplateSelector : DataTemplateSelector
		{
		public DataTemplate GeometryTemplate { get; set; }
		public DataTemplate DrawingTemplate { get; set; }

		public override DataTemplate SelectTemplate ( object item, DependencyObject container )
			{
			if ( item is PathGeometry || item is DrawingImage )
				{
				return item is PathGeometry ? GeometryTemplate : DrawingTemplate;
				}

			return base.SelectTemplate ( item, container );
			}
		}
	}
