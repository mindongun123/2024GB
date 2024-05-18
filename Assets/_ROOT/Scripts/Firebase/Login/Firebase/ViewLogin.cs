// #if UNITY_FIREBASE

using EasyTransition;
using MJGame;
using TMPro;
using UnityEngine;

namespace MJGame.MergeMerchant.Firebase
{
    public class ViewLogin : SingletonComponent<ViewLogin>
    {
        [Space]
        [Header("Registration")]
        public TMP_InputField nameRegisterField;
        public TMP_InputField emailRegisterField;
        [SerializeField] FirebaseAuthManager firebaseAuthManager;
        [SerializeField] GameObject loading;
        [SerializeField] DemoLoadScene demoLoadScene;

        public void Register()
        {
            LoadingEnable(true);
            StartCoroutine(firebaseAuthManager.RegisterAsync(nameRegisterField.text, emailRegisterField.text, "123456789"));
        }

        public void LoadingEnable(bool _true = false)
        {
            loading.SetActive(_true);
        }

        public void LoadScene()
        {
            demoLoadScene.LoadScene("Lobby");
        }
    }
}
// #endif
