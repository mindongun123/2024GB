namespace Ilumisoft.VolumeControl.UI
{
    public class MusicVolumeToggle : VolumeToggle
    {
        protected override bool IsOn
        {
            get => VolumeControl.Music.IsMuted;
            set => VolumeControl.Music.IsMuted = value;
        }
    }
}