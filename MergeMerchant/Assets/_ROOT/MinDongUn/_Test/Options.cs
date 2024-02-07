using System.Collections;
using System.Collections.Generic;
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
            set => _id = value;
            get => _id;
        }


        private void Start()
        {
            Setting(Random.Range(1, 3));
        }


        public void Setting(int _id)
        {
            ID = _id;
            image.sprite = sprites[ID];
        }
    }
}