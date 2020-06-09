using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager_Gameplay : MonoBehaviour
{
    float number;
    float numberPerSecond;

    public Button pause;

    public ClickButton clickButton;

    public GameObject pauseMenu;

    public TextMeshProUGUI numberText;
    public TextMeshProUGUI nPSText;

    public UpgradeManager upgradeManager;

    public static event Action<bool> onPause;

    void OnEnable()
    {
        ClickButton.onNumberIncrease += UpdateNumberText;
        ClickButton.onUpgradePayment += UpdateNumberText;

        UpgradeManager.onNumberUpdate += UpdateNumberText;
        UpgradeManager.onNPSUpdate += UpdateNPSText;
    }

    void Start()
    {
        number = 0;
        numberPerSecond = 0;

        numberText.text = "$" + number.ToString();
        nPSText.text = "$" + numberPerSecond.ToString("F1") + " p/second";
    }

    void OnDisable()
    {
        ClickButton.onNumberIncrease -= UpdateNumberText;
        ClickButton.onUpgradePayment -= UpdateNumberText;

        UpgradeManager.onNumberUpdate -= UpdateNumberText;
        UpgradeManager.onNPSUpdate -= UpdateNPSText;
    }

    public void Pause(bool state)
    {
        pauseMenu.SetActive(state);

        if (onPause != null)
            onPause(state);
    }

    #region Gameplay
    void UpdateNumberText()
    {
        number = clickButton.number;

        if (number == Mathf.Round(number))
            numberText.text = "$" + number.ToString();
        else
            numberText.text = "$" + number.ToString("F1");
    }

    void UpdateNumberText(float increment)
    {
        number = clickButton.number;

        if (number == Mathf.Round(number))
            numberText.text = "$" + number.ToString();
        else
            numberText.text = "$" + number.ToString("F1");
    }

    void UpdateNPSText()
    {
        numberPerSecond = upgradeManager.numberPerSecond;
        nPSText.text = "$" + numberPerSecond.ToString("F1") + " p/second";
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