using UnityEngine;
using System;

public class UpgradeController : MonoBehaviour
{
    public UpgradeModel model;

    GameManager gameManager;

    public static event Action<float, float> onPurchase;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void Purchase()
    {
        if (gameManager.money >= model.Cost)
        {
            model.IncreaseAmount();

            if (onPurchase != null)
                onPurchase(model.Cost, model.MoneyPerSecond);
        }
    }
}