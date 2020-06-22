using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager_Gameplay : MonoBehaviour
{
    float money;
    float moneyPerSecond;

    public Button pause;
    public GameManager gameManager;
    public GameObject pauseMenu;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI mPSText;

    public static event Action<bool> onPause;
    public static event Action onGameplayExit;

    void OnEnable()
    {
        GameManager.onMoneyIncrease += UpdateMoneyText;
        GameManager.onUpgradePayment += UpdateMoneyText;
        SaveManager.onGameplayLoaded += InitializeTexts;
        GameManager.onMoneyUpdate += UpdateMoneyText;
        GameManager.onMPSUpdate += UpdateMPSText;
    }

    void OnDisable()
    {
        GameManager.onMoneyIncrease -= UpdateMoneyText;
        GameManager.onUpgradePayment -= UpdateMoneyText;
        SaveManager.onGameplayLoaded -= InitializeTexts;
        GameManager.onMoneyUpdate -= UpdateMoneyText;
        GameManager.onMPSUpdate -= UpdateMPSText;
    }

    public void Pause(bool state)
    {
        pauseMenu.SetActive(state);

        if (onPause != null)
            onPause(state);
    }

    #region Gameplay UI
    void InitializeTexts(SaveManager.SaveData saveData)
    {
        money = saveData.money;
        moneyPerSecond = saveData.moneyPerSecond;

        moneyText.text = "$" + money;
        mPSText.text = "$" + moneyPerSecond.ToString("F1") + " p/second";
    }

    void UpdateMoneyText(float newMoney)
    {
        money = newMoney;

        if (money == Mathf.Round(money))
            moneyText.text = "$" + money;
        else
            moneyText.text = "$" + money.ToString("F1");
    }

    void UpdateMPSText(float newMPS)
    {
        moneyPerSecond = newMPS;
        mPSText.text = "$" + moneyPerSecond.ToString("F1") + " p/second";
    }
    #endregion

    #region Pause Menu
    public void ExitGameplay(bool alsoExitGame)
    {
        if (onGameplayExit != null)
            onGameplayExit();

        if (!alsoExitGame)
            SceneManager.LoadScene("Main Menu");
        else
            Application.Quit();
    }
    #endregion
}