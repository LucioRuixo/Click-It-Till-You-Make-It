using UnityEngine;
using TMPro;
using System;

public class Upgrade : MonoBehaviour
{
    [HideInInspector] public string _name;

    [HideInInspector] public int amount;
    
    [HideInInspector] public float numberPerSecond;
    [HideInInspector] public float cost;

    ClickButton clickButton;

    TextMeshProUGUI nameText;
    TextMeshProUGUI amountText;
    TextMeshProUGUI numberPerSecondText;
    TextMeshProUGUI costText;

    public static event Action<float, float> onPurchase;

    void Start()
    {
        clickButton = GameObject.Find("Click Button").GetComponent<ClickButton>();

        InitializeText();
    }

    void InitializeText()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform line = transform.GetChild(i).transform;

            for (int j = 0; j < line.childCount; j++)
            {
                switch (line.GetChild(j).name)
                {
                    case "Name":
                        nameText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        nameText.text = _name;
                        break;
                    case "Amount":
                        amountText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        amountText.text = amount.ToString();
                        break;
                    case "Number Per Second":
                        numberPerSecondText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        numberPerSecondText.text = numberPerSecond.ToString("F2") + " p/second";
                        break;
                    case "Cost":
                        costText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        costText.text = "$" + cost.ToString();
                        break;
                }
            }
        }
    }

    public void Purchase()
    {
        if (clickButton.number >= cost)
        {
            amount++;
            amountText.text = amount.ToString();

            if (onPurchase != null)
                onPurchase(cost, numberPerSecond);
        }
    }
}