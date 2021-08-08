using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int startingWave;
    public GameEvents gameEvents;

    private void OnEnable()
    {
        gameEvents.startWave.AddListener(StartWave);
        gameEvents.startWave.Invoke(startingWave);
    }
    
    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(StartWave);
    }

    private void StartWave(int waveNumber)
    {
        foreach (var child in transform)
            ((Transform)child).gameObject.SetActive(false);
        
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name != $"Wave ({waveNumber})") continue;
            child.gameObject.SetActive(true);
            break;
        }
    }
}
