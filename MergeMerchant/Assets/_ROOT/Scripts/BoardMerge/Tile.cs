using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


namespace MJGame.MergeMerchant.BoardMerge
{
    public class Tile : MonoBehaviour
    {

        [SerializeField] public Image img;
        [SerializeField] Sprite[] lsSprite;

        [SerializeField] GameObject spSelect;

        [SerializeField] Button btTile;
        private int _id;

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public void SetTile(int _id)
        {
            ID = _id;
            img.sprite = lsSprite[ID];
        }

        public void OnClick()
        {
            if (!IsOnClick()) return;
            SpriteSelect();
            SingletonComponent<TileController>.Instance.SetOnClick(this);
        }

        private bool IsOnClick()
        {
            if (ID != StaticGame.TILE_DEFAULT) return true;
            return false;
        }

        private void SpriteSelect(bool _isActive = true)
        {
            spSelect.SetActive(_isActive);
            btTile.interactable = !_isActive;
        }

        public void MergeTileComplete(bool _isUpdate = false)
        {
            SetTile(_isUpdate ? ID = ID + 1 : StaticGame.TILE_DEFAULT);
            SpriteSelect(false);
        }

        public void MergeNoComplete()
        {
            SpriteSelect(false);
        }
    }

}
