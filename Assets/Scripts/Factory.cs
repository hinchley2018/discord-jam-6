using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private int cost;
    [SerializeField] private AudioClip buildSound;
    [SerializeField] private float loseRate = 1;
    [SerializeField] private int loseCost = 1;
    
    private void Start()
    {
        FindObjectOfType<CurrencyManager>().AddReward(-cost);
        var factories = FindObjectsOfType<Factory>();
        if (buildSound) AudioPlayer.PlaySound(buildSound);
        //disabled for now, I'd like the snowman being placed to start the wave
        if (factories.Length == 1)
            gameEvents.startWave.Invoke(1);
        StartCoroutine(LoseIncome());
    }
    
    private IEnumerator LoseIncome()
    {
        //initial delay
        while (enabled)
        {
            yield return new WaitForSeconds(loseRate);
            var _currencyManager = FindObjectOfType<CurrencyManager>();
            _currencyManager.AddReward(-loseCost);
        }
        
    }
}
