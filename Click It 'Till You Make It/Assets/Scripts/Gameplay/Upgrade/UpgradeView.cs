using UnityEngine;
using TMPro;

public class UpgradeView : MonoBehaviour
{
    public UpgradeModel model;

    [HideInInspector] public TextMeshProUGUI amount;
    TextMeshProUGUI _name;
    TextMeshProUGUI moneyPerSecond;
    TextMeshProUGUI cost;

    void Start()
    {
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
                        _name = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        _name.text = model.Name;
                        break;
                    case "Amount":
                        amount = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        amount.text = model.Amount.ToString();
                        break;
                    case "Money Per Second":
                        moneyPerSecond = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        moneyPerSecond.text = model.MoneyPerSecond.ToString("F2") + " p/second";
                        break;
                    case "Cost":
                        cost = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        cost.text = "$" + model.Cost.ToString();
                        break;
                }
            }
        }
    }

    public void UpdateAmount()
    {
        amount.text = model.Amount.ToString();
    }
}