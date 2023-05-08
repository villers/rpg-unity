
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoginController : MonoBehaviour
{

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public RawImage background;


    void Start()
    {
        //background.SetLoginBackground();
    }

    void Update()
    {
        TabBehaviour();
    }

    private void TabBehaviour()
    {
        EventSystem currentEvent = EventSystem.current;

        if (currentEvent.currentSelectedGameObject == null || !Input.GetKeyDown(KeyCode.Tab))
            return;

        Selectable current = currentEvent.currentSelectedGameObject.GetComponent<Selectable>();
        if (current == null)
            return;

        bool up = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        Selectable next = up ? current.FindSelectableOnUp() : current.FindSelectableOnDown();
        next = current == next || next == null ? Selectable.allSelectablesArray[0] : next;
        currentEvent.SetSelectedGameObject(next.gameObject);
    }

    public void OnLoginClicked()
    {
        var username = usernameField.text;
        var password = passwordField.text;

        if (username.Length == 0 || password.Length == 0)
        {
            return;
        }

        TryConnectAndLogin(username, password);
    }

    public void OnExitClicked()
    {

    }

    private async void TryConnectAndLogin(string username, string password)
    {
        OnLoginResponse();
    }

    private void OnLoginResponse()
    {
       
    SceneManager.LoadSceneAsync("MapScene");
    }
}
