using UnityEngine;

public class GameManager : MonoBehaviour
{
    int upgradeAmount;

    public GameObject upgradePrefab;

    void Start()
    {
        upgradeAmount = 10;

        for (int i = 0; i < upgradeAmount; i++)
        {
            Instantiate(upgradePrefab).AddComponent<Upgrade>();
        }
    }

    void Update()
    {
        
    }
}
