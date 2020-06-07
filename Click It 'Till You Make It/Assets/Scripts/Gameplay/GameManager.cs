using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;

    public Transform upgradeButtonContainer;

    public static event Action<int> onUpgradeInitialization;

    [Header("Game properties")]
    public int upgradeAmount;

    void OnEnable()
    {
        onUpgradeInitialization += InitializeUpgrades;
    }

    void Start()
    {
        if (onUpgradeInitialization != null)
            onUpgradeInitialization(upgradeAmount);
    }

    void OnDisable()
    {
        onUpgradeInitialization -= InitializeUpgrades;
    }

    void InitializeUpgrades(int upgradeAmount)
    {
        for (int i = 0; i < upgradeAmount; i++)
        {
            Instantiate(upgradeButtonPrefab, upgradeButtonContainer).AddComponent<Upgrade>();
        }
    }
}