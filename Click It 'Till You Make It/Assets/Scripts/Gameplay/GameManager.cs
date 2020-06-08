using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gamePaused;

    void OnEnable()
    {
        UIManager_Gameplay.onPause += UpdatePauseState;
    }

    void OnDisable()
    {
        UIManager_Gameplay.onPause -= UpdatePauseState;
    }

    void UpdatePauseState(bool state)
    {
        gamePaused = state;
    }
}
