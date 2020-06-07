using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager_Gameplay : MonoBehaviour
{
    float number;

    public Button pause;

    public GameObject pauseMenu;

    public TextMeshProUGUI numberText;

    void OnEnable()
    {
        ClickButton.onNumberIncrease += UpdateNumberText;
    }

    void Start()
    {
        number = 0;
        numberText.text = number.ToString();
    }

    void OnDisable()
    {
        ClickButton.onNumberIncrease -= UpdateNumberText;
    }

    #region Gameplay
    public void UpdateNumberText(int value)
    {
        number = value;
        numberText.text = number.ToString();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    #endregion

    #region Pause Menu
    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}