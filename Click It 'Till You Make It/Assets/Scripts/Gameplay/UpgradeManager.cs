using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject upgradeButtonPrefab;
    public Transform upgradeButtonContainer;

    [HideInInspector] public List<UpgradeModel> upgrades;

    public static event Action<int> onUpgradeInitialization;

    [Header("Upgrade properties")]
    public List<UpgradeScriptableObject> upgradeScriptableObjects;

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

        upgrades = new List<UpgradeModel>();
        for (int i = 0; i < upgradeScriptableObjects.Count; i++)
        {
            upgrades.Add(Instantiate(upgradeButtonPrefab, upgradeButtonContainer).GetComponent<UpgradeModel>());

            upgrades[i].Name = upgradeScriptableObjects[i]._name;
            upgrades[i].Cost = upgradeScriptableObjects[i].cost;
            upgrades[i].MoneyPerSecond = upgradeScriptableObjects[i].moneyPerSecond;

            if (!onFileCreation)
                upgrades[i].Amount = saveData.upgradePurchasedAmount[i];
        }

        if (onUpgradeInitialization != null)
            onUpgradeInitialization(upgradeScriptableObjects.Count);
    }
}