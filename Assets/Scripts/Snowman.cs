using System.Collections;
using System;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int reward;
    [SerializeField] private int health;
    [SerializeField] private Transform target;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private bool IsInFactoryDamageRange = false;
    [SerializeField] private GameObject snowmanDeathPrefab;
    private float delay = .25F;
    private Coroutine coroutine;

    private void Start()
    {
        target = FindObjectOfType<TownCenter>().transform;
        AudioPlayer.PlaySound(spawnSound);
    }

    private void Update()
    {
        var direction = target.position - transform.position;
        direction = direction.normalized;
        transform.position += direction * (speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Factory>())
        {
            IsInFactoryDamageRange = true;
            coroutine = StartCoroutine(CheckForDamage());
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Factory>())
        {
            if (coroutine != null) StopCoroutine(coroutine);
            IsInFactoryDamageRange = false;
        }
    }

    private IEnumerator CheckForDamage()
    {
        while (IsInFactoryDamageRange)
        {
            if (health == 0)
            {
                var _currencyManager = FindObjectOfType<CurrencyManager>();
                _currencyManager.AddReward(reward);
                Instantiate(snowmanDeathPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            health -= 1;
            yield return new WaitForSeconds(delay);
        }
    }
    
}