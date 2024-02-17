using System.Collections;
using System.Collections.Generic;
using MJGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Mindongun
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

        public void Setting(int _id)
        {
            ID = _id;
            image.sprite = sprites[ID];
        }

        public void SetBasketSave()
        {
            basketButton.enabled = true;
            IDBasket = ID % 10;
            _isBasket = true;

            AnimatorBasket();
        }
    }
}