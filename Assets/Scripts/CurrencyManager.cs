using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private Text _currencyText;
    public int money;

    private void Start()
    {
        AddReward(0);
    }

    public void AddReward(int reward)
    {
        money += reward;
        _currencyText.text = $"Currency: {money}";
    }
}
