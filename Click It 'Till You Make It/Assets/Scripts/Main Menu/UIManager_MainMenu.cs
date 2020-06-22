using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager_MainMenu : MonoBehaviour
{
    public Button play;
    public Button credits;
    public Button exit;
    public GameObject mainScreen;
    public GameObject savesScreen;
    public GameObject creditsScreen;

    public static event Action<int> onGameLoad;

    public void ReturnToMainScreen(GameObject currentScreen)
    {
        currentScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    #region Main Screen

    public void ShowSavesScene()
    {
        mainScreen.SetActive(false);
        savesScreen.SetActive(true);
    }

    public void ShowCreditsScreen()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion

    #region Saves Screen
    public void LoadGame(int saveNumber)
    {
        if (onGameLoad != null)
            onGameLoad(saveNumber);

        SceneManager.LoadScene("Gameplay");
    }
    #endregion
}