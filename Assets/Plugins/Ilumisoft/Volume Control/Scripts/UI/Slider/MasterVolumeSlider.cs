namespace Ilumisoft.VolumeControl.UI
{
    public class MasterVolumeSlider : VolumeSlider
    {
        protected override float Volume
        {
            get => VolumeControl.Master.Volume;
            set => VolumeControl.Master.Volume = value;
        }
    }
}