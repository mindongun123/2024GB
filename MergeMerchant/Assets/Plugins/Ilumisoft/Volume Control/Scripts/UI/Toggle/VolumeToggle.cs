namespace Ilumisoft.VolumeControl.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Toggle))]
    public abstract class VolumeToggle : MonoBehaviour
    {
        protected abstract bool IsOn
        {
            get; set;
        }

        protected Toggle toggle;

        protected virtual void Awake()
        {
            this.toggle = GetComponent<Toggle>();
        }

        protected virtual void Start()
        {
            this.toggle.isOn = IsOn;

            this.toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        protected virtual void OnToggleValueChanged(bool value)
        {
            IsOn = value;
        }
    }
}