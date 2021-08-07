using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _currencyText;
    [SerializeField] private int money;
    // Start is called before the first frame update
    
    public void AddReward(int reward)
    {
        money += reward;
        _currencyText.text = $"Currency: {money}";
    }
}
