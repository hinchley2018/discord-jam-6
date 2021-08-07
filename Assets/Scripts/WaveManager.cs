using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameEvents gameEvents;

    private void OnEnable()
    {
        gameEvents.startWave.AddListener(StartWave);
        gameEvents.startWave.Invoke(0);
    }
    
    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(StartWave);
    }

    private void StartWave(int arg0)
    {
        Debug.Log("WaveManager: Start Wave Here!");
    }
}
