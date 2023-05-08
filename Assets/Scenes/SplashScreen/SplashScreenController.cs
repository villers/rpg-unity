using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI labelText;


    void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        labelText.text = "Checking for updates...";
        yield return Addressables.InitializeAsync();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("LoginScene");
    }

}
