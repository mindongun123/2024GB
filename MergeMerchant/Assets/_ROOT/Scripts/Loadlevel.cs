using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlevel : MonoBehaviour
{
    [SerializeField] GameObject setactive;
    public void OnClickLoadLevel(bool _ac)
    {
        setactive.SetActive(_ac);
    }
}
