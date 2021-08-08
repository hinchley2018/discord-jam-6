using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int startingWave;
    public GameEvents gameEvents;

    private void OnEnable()
    {
        gameEvents.startWave.AddListener(StartWave);
        gameEvents.gameOver.AddListener(SlowDownTime);
        //gameEvents.startWave.Invoke(startingWave);
    }
    
    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(StartWave);
        gameEvents.gameOver.RemoveListener(SlowDownTime);
    }

    private void StartWave(int waveNumber)
    {
        Debug.Log($"WaveManager: Beginning of {waveNumber}");
        if (waveNumber == 0) Main.PlayHappyMusic();
        if (waveNumber == 1) Main.PlayAngryMusic();
        
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

    private void SlowDownTime()
    {
        Time.timeScale = 0.1f;
    }
}
