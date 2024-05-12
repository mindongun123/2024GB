namespace Ilumisoft.VolumeControl.Core
{
    using UnityEngine;

    public static class Utils
    {
        /// <summary>
        /// Converts a given value from [0,1] to decibel [-80, 0].
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ConvertToDecibel(float value)
        {
            value = Mathf.Clamp01(value);

            float min = 0.0001f;        //0.0001 equals -80dB
            float max = 1;              //1 equals 0dB

            //Scale volume
            float linearValue = Mathf.Lerp(min, max, value);

            //Convert volume to decibel
            float dBValue = 20 * Mathf.Log10(linearValue);

            return dBValue;
        }

        /// <summary>
        /// Converts a given decibel value from [-80, 0] to a non decibel one [0, 1].
        /// This is the inverse method to ConvertToDecibel(float value)
        /// </summary>
        /// <param name="dBValue"></param>
        /// <returns></returns>
        public static float ConvertFromDecibel(float dBValue)
        {
            dBValue = Mathf.Clamp(dBValue, -80, 0);

            float linearValue = Mathf.Pow(10, dBValue / 20);

            float min = 0.0001f;        //0.0001 equals -80dB
            float max = 1;              //1 equals 0dB

            float volume = Mathf.InverseLerp(min, max, linearValue);

            return volume;
        }
    }
}