using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHM.Helpers
	{
	[Serializable]
	public class PdfData
		{
		public string FilePath { get; set; }
		public FileStream FileStream { get; set; }

		}
	}
