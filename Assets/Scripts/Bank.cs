using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int income;
    private bool exists = true;
    private void Start()
    {
        StartCoroutine(EarnIncome());
    }

    private IEnumerator EarnIncome()
    {
        //initial delay
        while (exists)
        {
            yield return new WaitForSeconds(2);
            var _currencyManager = FindObjectOfType<CurrencyManager>();
            _currencyManager.AddReward(income);
            yield return new WaitForSeconds(2);
        }
        
    }
    
}
