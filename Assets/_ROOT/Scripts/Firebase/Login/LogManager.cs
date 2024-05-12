using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance;

    [SerializeField]
    private Text _warningText;

    private void Awake()
    {
        if(Instance == null)

        {
            Instance = this;
        }
    }

    private void Start()
    {
        _warningText.gameObject.SetActive(false);
    }

    public void ShowWarningText(string message)
    {
        _warningText.text = message;

        StartCoroutine(ShowMessageAsync());
    }

    private IEnumerator ShowMessageAsync()
    {
        _warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _warningText.gameObject.SetActive(false);
    }
}
