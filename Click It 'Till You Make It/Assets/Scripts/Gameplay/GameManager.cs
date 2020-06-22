using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gamePaused;

    public float money;
    public float moneyPerSecond;
    float tickTimer;
    float tickDuration;

    public static event Action<float> onMoneyIncrease;
    public static event Action<float> onUpgradePayment;
    public static event Action<float> onMoneyUpdate;
    public static event Action<float> onMPSUpdate;

    void OnEnable()
    {
        SaveManager.onGameplayLoaded += InitializeValues;

        UIManager_Gameplay.onPause += UpdatePauseState;

        UpgradeController.onPurchase += UpdateMPSOnPurchase;
        UpgradeController.onPurchase += PayForUpgrade;
    }

    void Start()
    {
        tickTimer = 0f;
        tickDuration = 1f;
    }

    void Update()
    {
        if (!gamePaused)
        {
            tickTimer += Time.deltaTime;

            if (tickTimer >= tickDuration)
            {
                IncreaseMoneyOnTick();
            }
        }
    }

    void OnDisable()
    {
        SaveManager.onGameplayLoaded -= InitializeValues;

        UIManager_Gameplay.onPause -= UpdatePauseState;

        UpgradeController.onPurchase -= UpdateMPSOnPurchase;
        UpgradeController.onPurchase -= PayForUpgrade;
    }

    void InitializeValues(SaveManager.SaveData saveData)
    {
        money = saveData.money;
        moneyPerSecond = saveData.moneyPerSecond;
    }

    void IncreaseMoneyOnTick()
    {
        money += moneyPerSecond;

        if (onMoneyUpdate != null)
            onMoneyUpdate(money);

        tickTimer -= tickDuration;
    }

    void UpdateMPSOnPurchase(float cost, float purchasedMPS)
    {
        moneyPerSecond += purchasedMPS;

        if (onMPSUpdate != null)
            onMPSUpdate(moneyPerSecond);
    }

    void PayForUpgrade(float cost, float purchasedMPS)
    {
        money -= cost;

        if (onUpgradePayment != null)
            onUpgradePayment(money);
    }

    void UpdatePauseState(bool state)
    {
        gamePaused = state;
    }

    public void IncreaseMoneyOnClick()
    {
        money++;

        if (onMoneyIncrease != null)
            onMoneyIncrease(money);
    }
}