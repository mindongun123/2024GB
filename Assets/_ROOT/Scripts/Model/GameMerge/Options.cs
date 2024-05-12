using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.Merge
{
    public partial class Options : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        [SerializeField] Sprite[] sprites;
        public Image image;

        private int _id;
        public int ID
        {
            set => _id = value < 69 ? value : 68;
            get => _id;
        }

        public void Setting(int _id, Vector2Int kPosMax = default)
        {
            ID = _id;
            if (ID % 10 == 0 && ID / 10 > 0)
            {
                DestroyOptionIDMax(kPosMax);
                return;
            }
            image.sprite = sprites[ID];
        }

        public void SetBasketSave()
        {
            basketButton.enabled = true;
            ID = SingletonComponent<SaveGameMerge>.Instance.IdBasket;
            IDBasket = ID % 10;
            _isBasket = true;
            image.sprite = sprites[ID];
            AnimatorBasket();
        }


        private void DestroyOptionIDMax(Vector2Int kPosMax)
        {
            SingletonComponent<VFXParticleItem>.Instance.OnClickItemVFX(transform.position, 5, NameItem.diamond);
            ViewReward.AddDiamond(10);
            SingletonComponent<BFS>.Instance.SetGridAtPosition(kPosMax, 0);
            Destroy(gameObject);
        }
    }
}