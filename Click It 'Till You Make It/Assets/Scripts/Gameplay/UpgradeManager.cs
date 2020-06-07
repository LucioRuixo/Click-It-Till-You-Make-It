using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;

    public Transform upgradeButtonContainer;

    public List<Upgrade> upgrades;

    public static event Action<int> onUpgradeInitialization;

    [Header("Upgrade properties")]
    public int amount;

    public float baseCost;
    public float costMultiplier;
    public float baseNPS;
    public float nPSMultiplier;

    public List<string> names;

    void OnEnable()
    {
        onUpgradeInitialization += InitializeUpgrades;
    }

    void Start()
    {
        if (onUpgradeInitialization != null)
            onUpgradeInitialization(amount);
    }

    void OnDisable()
    {
        onUpgradeInitialization -= InitializeUpgrades;
    }

    void InitializeUpgrades(int upgradeAmount)
    {
        for (int i = 0; i < upgradeAmount; i++)
        {
            upgrades.Add(Instantiate(upgradeButtonPrefab, upgradeButtonContainer).AddComponent<Upgrade>());

            if (i == 0)
            {
                upgrades[i].cost = baseCost;
                upgrades[i].numberPerSecond = baseNPS;
            }
            else
            {
                upgrades[i].cost = upgrades[i - 1].cost * costMultiplier;
                upgrades[i].numberPerSecond = upgrades[i - 1].numberPerSecond * nPSMultiplier;
            }

            if (i < names.Count)
                upgrades[i]._name = names[i];
            else
                upgrades[i]._name = "Upgrade " + (i + 1).ToString();
        }
    }
}