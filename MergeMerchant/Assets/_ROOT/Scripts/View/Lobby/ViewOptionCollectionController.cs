using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MJGame.MergeMerchant.Lobby;
using Sirenix.OdinInspector;
using UnityEngine;
namespace MJGame.MergeMerchant.Lobby
{

    public class ViewOptionCollectionController : MonoBehaviour
    {

        public Sprite[] sprites;
        [SerializeField]
        ViewItemOptionCollection[] viewItems;
        [SerializeField] SaveLobbyGame saveLobbyGame;
        private void OnEnable()
        {
            SetSpriteOption();
        }

        public void SetSpriteOption()
        {
            List<Vector4> ls = saveLobbyGame.GetListViewOption();

            foreach (var item in ls)
            {
                int _id = (int)item.x;
                int _complete = (int)item.z;
                int _open = (int)item.y;
                int _reward = (int)item.w;
                viewItems[_id - 1].SetViewItemCollection(_id, _open, _complete, _reward, sprites[_id]);
            }
        }

        [Button]
        public void TestAddListOpen(Vector4 _vt)
        {
            // choi an them 1 cai 
            saveLobbyGame.SetListViewOption(_vt);
        }
    }

}