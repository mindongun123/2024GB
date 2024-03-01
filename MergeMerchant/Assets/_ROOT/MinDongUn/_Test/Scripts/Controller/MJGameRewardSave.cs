using MJGame.MergeMerchant.Merge;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace MJGame.MergeMerchant
{
    public static class ViewReward
    {
        public static UnityAction<int> OnCoinChanged = null;
        public static UnityAction<int> OnDiamondChange = null;
        public static UnityAction<int> OnEnergyChange = null;
        public static UnityAction<int> OnChangeCommon = null;

        public static int Diamond
        {
            get => PlayerPrefs.GetInt(ConstGame.DIAMOND, 20);
            set => PlayerPrefs.SetInt(ConstGame.DIAMOND, value);
        }


        public static int Coin
        {
            get => PlayerPrefs.GetInt(ConstGame.COIN, 100);
            set => PlayerPrefs.SetInt(ConstGame.COIN, value);
        }

        public static int Energy
        {
            get => PlayerPrefs.GetInt(ConstGame.ENERGY, 100);
            set => PlayerPrefs.SetInt(ConstGame.ENERGY, value);
        }

        public static void Reset()
        {
            Diamond = 0;
            Energy = 100;
            Coin = 100;

            OnCoinChanged?.Invoke(Coin);
            OnDiamondChange?.Invoke(Diamond);
            OnEnergyChange?.Invoke(Energy);
        }

        public static void AddCoin(int value)
        {
            if (Coin + value < 0)
            {
                Debug.Log("Coin Gioi han am");
                return;
            }
            Coin += value;
            OnCoinChanged?.Invoke(Coin);
        }

        public static void AddDiamond(int value)
        {
            if (Diamond + value < 0)
            {
                Debug.Log("Diamond Gioi han am");
                return;
            }
            Diamond += value;
            OnDiamondChange?.Invoke(Diamond);
        }

        public static bool AddEnergy(int value)
        {
            if (Energy + value < 0)
            {
                Debug.Log("Energy Gioi han am");
                return false;
            }
            Energy += value;
            OnEnergyChange?.Invoke(Energy);
            return true;
        }

        public static void AddCommon(TextMeshProUGUI text, int value, int type)
        {
            type += value;
            OnChangeCommon?.Invoke(type);
        }
    }
}