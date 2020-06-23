using UnityEngine;
using UnityEngine.UI;

public class CoinsSFX : MonoBehaviour
{
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();

        GetComponent<Button>().onClick.AddListener(delegate { soundManager.PlaySound(1); });
    }
}