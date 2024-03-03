namespace Ilumisoft.VolumeControl.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public abstract class VolumeSlider : MonoBehaviour
    {
        protected abstract float Volume
        {
            get; set;
        }

        protected Slider slider;

        protected virtual void Awake()
        {
            this.slider = GetComponent<Slider>();
        }

        protected virtual void Start()
        {
            this.slider.value = Volume;

            this.slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        protected virtual void OnSliderValueChanged(float value)
        {
            Volume = value;
        }
    }
}