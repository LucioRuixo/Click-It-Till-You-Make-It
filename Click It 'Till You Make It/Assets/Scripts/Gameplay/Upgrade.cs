using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
    new public string name;
    
    public int amount;
    
    public float numberPerSecond;
    public float cost;
    
    TextMeshProUGUI nameText;
    TextMeshProUGUI amountText;
    TextMeshProUGUI numberPerSecondText;
    TextMeshProUGUI costText;

    void Start()
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
                        break;
                    case "Amount":
                        amountText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        break;
                    case "Number Per Second":
                        numberPerSecondText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        break;
                    case "Cost":
                        costText = line.GetChild(j).GetComponent<TextMeshProUGUI>();
                        break;
                }
            }
        }

        UpdateText();
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        nameText.text = name;
        amountText.text = amount.ToString();
        costText.text = "$" + cost.ToString();
        numberPerSecondText.text = numberPerSecond.ToString() + " p/second";
    }

    public void Buy()
    {
        amount++;

        amountText.text = amount.ToString();
    }
}