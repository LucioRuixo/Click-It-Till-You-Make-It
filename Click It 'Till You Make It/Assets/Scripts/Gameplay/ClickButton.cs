using System;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    int number;

    public static event Action<int> onNumberIncrease;

    void Start()
    {
        number = 0;
    }

    public void IncreaseNumber()
    {
        number++;

        if (onNumberIncrease != null)
            onNumberIncrease(number);
    }
}
