using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    float updateTimer;
    float updateTimerTarget;
    public float numberPerSecond;

    public GameManager gameManager;

    public GameObject upgradeButtonPrefab;

    public Transform upgradeButtonContainer;

    public List<Upgrade> upgrades;

    public static event Action<int> onUpgradeInitialization;
    public static event Action<float> onNumberUpdate;
    public static event Action onNPSUpdate;

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
        Upgrade.onPurchase += UpdateNPS;
    }

    void Start()
    {
        updateTimer = 0f;
        updateTimerTarget = 1f;
        numberPerSecond = 0f;

        if (onUpgradeInitialization != null)
            onUpgradeInitialization(amount);
    }

    void Update()
    {
        if (!gameManager.gamePaused)
        {
            updateTimer += Time.deltaTime;

            if (updateTimer >= updateTimerTarget)
            {
                if (onNumberUpdate != null)
                    onNumberUpdate(numberPerSecond);

                updateTimer -= updateTimerTarget;
            }
        }
    }

    void OnDisable()
    {
        onUpgradeInitialization -= InitializeUpgrades;
        Upgrade.onPurchase -= UpdateNPS;
    }

    void InitializeUpgrades(int upgradeAmount)
    {
        for (int i = 0; i < upgradeAmount; i++)
        {
            upgrades.Add(Instantiate(upgradeButtonPrefab, upgradeButtonContainer).GetComponent<Upgrade>());

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

    void UpdateNPS(float cost, float nPS)
    {
        numberPerSecond += nPS;

        if (onNPSUpdate != null)
            onNPSUpdate();
    }
}