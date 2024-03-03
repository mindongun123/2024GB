namespace Ilumisoft.VolumeControl.UI
{
    public class SFXVolumeToggle : VolumeToggle
    {
        protected override bool IsOn
        {
            get => VolumeControl.SFX.IsMuted;
            set => VolumeControl.SFX.IsMuted = value;
        }
    }
}