using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private int cost;

    private void Start()
    {
        FindObjectOfType<CurrencyManager>().AddReward(-cost);
        var factories = FindObjectsOfType<Factory>();
        if (factories.Length == 1)
            gameEvents.startWave.Invoke(1);
    }
}
