using System;
using UnityEngine;

[Serializable]
public class UpgradeModel : MonoBehaviour
{
    public UpgradeView view;

    [HideInInspector] public string Name;

    [HideInInspector] public int Amount;

    [HideInInspector] public float Cost;
    [HideInInspector] public float MoneyPerSecond;

    public void IncreaseAmount()
    {
        Amount++;
        view.UpdateAmount();
    }
}