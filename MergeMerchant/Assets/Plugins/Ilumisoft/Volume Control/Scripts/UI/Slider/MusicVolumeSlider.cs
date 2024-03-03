namespace Ilumisoft.VolumeControl.UI
{
    public class MusicVolumeSlider : VolumeSlider
    {
        protected override float Volume
        {
            get => VolumeControl.Music.Volume;
            set => VolumeControl.Music.Volume = value;
        }
    }
}