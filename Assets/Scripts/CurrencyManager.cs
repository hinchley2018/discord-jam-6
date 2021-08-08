using UnityEngine;
using TMPro;
public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;
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
