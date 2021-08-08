using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Snowman : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int reward;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetWithRandomness;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private bool IsInFactoryDamageRange = false;
    [SerializeField] private GameObject snowmanDeathPrefab;
    [SerializeField] private float stopRadius = 1;
    private float delay = .25F;
    private Coroutine coroutine;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        target = FindObjectOfType<TownCenter>().transform;
        targetWithRandomness = target.position + (Vector3) Random.insideUnitCircle;
        if (spawnSound) AudioPlayer.PlaySound(spawnSound);
    }

    private void Update()
    {
        _slider.value = health / maxHealth;
        var delta = transform.position - target.position;
        var distanceToTarget = delta.magnitude;
        if (distanceToTarget <= stopRadius) return;
        var direction = targetWithRandomness - transform.position;
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