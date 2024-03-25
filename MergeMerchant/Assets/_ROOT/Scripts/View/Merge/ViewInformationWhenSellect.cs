using TMPro;
using UnityEngine;


namespace MJGame.MergeMerchant.Merge
{
    public class ViewInformationWhenSellect : SingletonComponent<ViewInformationWhenSellect>
    {
        [SerializeField] TextMeshProUGUI txtInfo;
        [SerializeField] GameObject btnRemove;
        [SerializeField] GameObject btnAdmob;
        [SerializeField] TextMeshProUGUI txtMoney;
        [SerializeField] TextMeshProUGUI txtDiamond;
        int _money;
        int _diamond;

        private void OnEnable()
        {
            SetOptionButton(true);
        }
        public void ShowInformationText(bool _isBasket, int _id, Options options, Transform parent)
        {
            SetOptionButton(_isBasket);

            if (!_isBasket)
            {

                txtInfo.text = "<color=\"red\">Message: </color>" + STRING.GetStringRemveOption();
                SetOptionRemove(options, parent.GetComponent<TileBaseOptions>());
                _money = -Random.Range(1, 5) * _id;
                txtMoney.text = _money.ToString();
            }
            else
            {
                txtInfo.text = "<color=\"red\">AdMob: </color>" + STRING.GetStringAdMobOption();
                _diamond = Random.Range(10, 50);
                txtDiamond.text = _diamond.ToString();
            }
        }

        private void SetOptionButton(bool _is)
        {
            btnAdmob.SetActive(_is);
            btnRemove.SetActive(!_is);
        }

        Options optionRemove;
        Vector2Int vtRemove;
        int _remove;

        private void SetOptionRemove(Options ops, TileBaseOptions tileBaseOptions)
        {
            Vector2Int _ps = SingletonComponent<MergeOptionsController>.Instance.GetIdTileBaseOptions(tileBaseOptions);
            optionRemove = ops;
            vtRemove = _ps;
            _remove = ops.ID * 10 * Random.Range(1, 5);
        }

        public void OnClickRemoveOption()
        {
            if (ViewReward.AddCoin(_money))
            {
                Destroy(optionRemove.gameObject);
                SingletonComponent<BFS>.Instance.SetGridAtPosition(vtRemove, 0);
                SetOptionButton(true);
                txtInfo.text = "<color=\"red\">AdMob: </color>" + STRING.GetStringAdMobOption();
            }

        }

        public void OnClickRewardAdmob()
        {
            print("admob");
        }
    }
}
