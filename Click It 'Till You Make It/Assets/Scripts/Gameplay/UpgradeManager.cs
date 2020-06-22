using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject upgradeButtonPrefab;
    public Transform upgradeButtonContainer;

    public List<UpgradeModel> upgrades;

    public static event Action<int> onUpgradeInitialization;

    [Header("Upgrade properties")]
    public int upgradeAmount;

    public float baseCost;
    public float costMultiplier;
    public float baseMPS;
    public float mPSMultiplier;

    public List<string> names;

    void OnEnable()
    {
        SaveManager.onGameplayLoaded += InitializeUpgrades;
    }

    void OnDisable()
    {
        SaveManager.onGameplayLoaded -= InitializeUpgrades;
    }

    void InitializeUpgrades(SaveManager.SaveData saveData)
    {
        bool onFileCreation;
        if (saveData.upgradePurchasedAmount.Count == 0)
            onFileCreation = true;
        else
            onFileCreation = false;

        for (int i = 0; i < upgradeAmount; i++)
        {
            upgrades.Add(Instantiate(upgradeButtonPrefab, upgradeButtonContainer).GetComponent<UpgradeModel>());

            if (i == 0)
            {
                upgrades[i].Cost = baseCost;
                upgrades[i].MoneyPerSecond = baseMPS;
            }
            else
            {
                upgrades[i].Cost = upgrades[i - 1].Cost * costMultiplier;
                upgrades[i].MoneyPerSecond = upgrades[i - 1].MoneyPerSecond * mPSMultiplier;
            }

            if (!onFileCreation)
                upgrades[i].Amount = saveData.upgradePurchasedAmount[i];

            if (i < names.Count)
                upgrades[i].Name = names[i];
            else
                upgrades[i].Name = "Upgrade " + (i + 1).ToString();
        }

        if (onUpgradeInitialization != null)
            onUpgradeInitialization(upgradeAmount);
    }
}