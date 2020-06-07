using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager_MainMenu : MonoBehaviour
{
    public Button play;
    public Button credits;
    public Button exit;

    public GameObject mainScreen;
    public GameObject creditsScreen;

    #region Main Screen
    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowCredits()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion

    #region Credits Screen
    public void Return()
    {
        creditsScreen.SetActive(false);
        mainScreen.SetActive(true);
    }
    #endregion
}