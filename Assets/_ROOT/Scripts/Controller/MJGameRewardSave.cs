using Ilumisoft.VisualStateMachine;
using MJGame.MergeMerchant.Lobby;
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
            get => PlayerPrefs.GetInt(ConstGame.DIAMOND, 20000);
            set => PlayerPrefs.SetInt(ConstGame.DIAMOND, value);
        }


        public static int Coin
        {
            get => PlayerPrefs.GetInt(ConstGame.COIN, 10000);
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

        public static bool AddCoin(int value)
        {

            if (Coin + value < 0)
            {
                Debug.Log("Coin Gioi han am");
                return false;
            }
            if (value > 0)
            {
                SingletonComponent<AudioController>.Instance.AudioRewardPlay();
            }
            Coin += value;
            OnCoinChanged?.Invoke(Coin);

            ConfigNotice.eventNoticeBuild?.Invoke();

            return true;
        }

        public static bool AddDiamond(int value)
        {
            if (Diamond + value < 0)
            {
                Debug.Log("Diamond Gioi han am");
                return false;
            }
            if (value > 0)
            {
                SingletonComponent<AudioController>.Instance.AudioRewardPlay();
            }
            Diamond += value;
            OnDiamondChange?.Invoke(Diamond);
            return true;
        }

        public static bool AddEnergy(int value)
        {
            if (Energy + value < 0)
            {
                SingletonComponent<SpawnText>.Instance.NewText("<color=yellow>no Energy</color>", 1f);
                return false;
            }
            if (value > 0)
            {
                SingletonComponent<AudioController>.Instance.AudioRewardPlay();
            }
            Energy += value;
            OnEnergyChange?.Invoke(Energy);
            return true;
        }

    }
}