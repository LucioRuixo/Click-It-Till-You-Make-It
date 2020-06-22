using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeScriptableObject : ScriptableObject
{
    public string _name;

    public float cost;
    public float moneyPerSecond;
}