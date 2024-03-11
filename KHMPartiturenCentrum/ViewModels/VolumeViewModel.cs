namespace KHM.ViewModels
{
	public partial class VolumeViewModel
	{
		private double selectedVolume;

		public double SelectedVolume
		{
			get
			{
				return selectedVolume;
			}
			set
			{
				selectedVolume = value;
			}
		}
	}
}
