using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Snowman : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private int reward;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 targetWithRandomness;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private GameObject snowmanDeathPrefab;
    [SerializeField] private float stopRadius = 1;
    [SerializeField] private float delayBetweenTakeDamage = .25F;
    [SerializeField] public int attackDamage = 1;
    private Coroutine coroutine;
    private Slider _slider;
    private List<Factory> factoriesNearMe = new List<Factory>();
    private CurrencyManager _currencyManager;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    private void Start()
    {
        target = FindObjectOfType<TownCenter>().transform;
        targetWithRandomness = target.position + (Vector3) Random.insideUnitCircle;
        if (spawnSound) AudioPlayer.PlaySound(spawnSound);
        coroutine = StartCoroutine(CheckForDamage());
    }

    private void OnDestroy()
    {
        if (coroutine != null) StopCoroutine(CheckForDamage());
        coroutine = null;
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
        var factory = other.gameObject.GetComponent<Factory>();
        if (factory)
            factoriesNearMe.Add(factory);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var factory = other.gameObject.GetComponent<Factory>();
        if (factory)
            factoriesNearMe.Remove(factory);
    }

    private IEnumerator CheckForDamage()
    {
        while (enabled)
        {
            if (health <= 0)
            {
                _currencyManager.AddReward(reward);
                Instantiate(snowmanDeathPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            if (factoriesNearMe.Count > 0)
            {
                health -= factoriesNearMe.Count * 1;
                yield return new WaitForSeconds(delayBetweenTakeDamage);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}