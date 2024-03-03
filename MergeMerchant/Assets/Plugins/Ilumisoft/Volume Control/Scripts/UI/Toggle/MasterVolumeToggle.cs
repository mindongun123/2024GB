namespace Ilumisoft.VolumeControl.UI
{
    public class MasterVolumeToggle : VolumeToggle
    {
        protected override bool IsOn
        {
            get => VolumeControl.Master.IsMuted;
            set => VolumeControl.Master.IsMuted = value;
        }
    }
}