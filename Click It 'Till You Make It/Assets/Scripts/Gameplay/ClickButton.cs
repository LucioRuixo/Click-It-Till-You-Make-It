using System;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public static float number;

    public static event Action onNumberIncrease;
    public static event Action onUpgradePayment;

    void OnEnable()
    {
        Upgrade.onPurchase += PayForUpgrade;

        UpgradeManager.onNumberUpdate += IncreaseNumber;
    }

    void Start()
    {
        number = 0;
    }

    void OnDisable()
    {
        Upgrade.onPurchase -= PayForUpgrade;

        UpgradeManager.onNumberUpdate -= IncreaseNumber;
    }

    void IncreaseNumber(float increment)
    {
        number += increment;
    }

    void PayForUpgrade(float cost, float nPS)
    {
        number -= cost;

        if (onUpgradePayment != null)
            onUpgradePayment();
    }

    public void IncreaseNumberOnClick()
    {
        number++;

        if (onNumberIncrease != null)
            onNumberIncrease();
    }
}