using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private int cost;

    private void Start()
    {
        FindObjectOfType<CurrencyManager>().AddReward(-cost);
    }
}
