using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager_Gameplay : MonoBehaviour
{
    float number;

    public Button pause;

    public GameObject pauseMenu;

    public TextMeshProUGUI numberText;

    public static event Action<bool> onPause;

    void OnEnable()
    {
        ClickButton.onNumberIncrease += UpdateNumberText;
        ClickButton.onUpgradePayment += UpdateNumberText;
        UpgradeManager.onNumberUpdate += UpdateNumberText;
    }

    void Start()
    {
        number = 0;
        numberText.text = number.ToString();
    }

    void OnDisable()
    {
        ClickButton.onNumberIncrease -= UpdateNumberText;
        ClickButton.onUpgradePayment -= UpdateNumberText;
        UpgradeManager.onNumberUpdate -= UpdateNumberText;
    }

    public void Pause(bool state)
    {
        pauseMenu.SetActive(state);

        if (onPause != null)
            onPause(state);
    }

    #region Gameplay
    public void UpdateNumberText()
    {
        number = ClickButton.number;

        if (number == Mathf.Round(number))
            numberText.text = number.ToString();
        else
            numberText.text = number.ToString("F1");
    }

    public void UpdateNumberText(float increment)
    {
        number = ClickButton.number;

        if (number == Mathf.Round(number))
            numberText.text = number.ToString();
        else
            numberText.text = number.ToString("F1");
    }
    #endregion

    #region Pause Menu
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