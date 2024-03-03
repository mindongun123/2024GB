namespace Ilumisoft.VolumeControl.UI
{
    public class SFXVolumeSlider : VolumeSlider
    {
        protected override float Volume
        {
            get => VolumeControl.SFX.Volume;
            set => VolumeControl.SFX.Volume = value;
        }
    }
}