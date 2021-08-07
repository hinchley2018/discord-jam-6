using System;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int reward;
    [SerializeField] private Transform target;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        target = FindObjectOfType<TownCenter>().transform;
        AudioPlayer.PlaySound(spawnSound);
    }

    private void OnDestroy()
    {
        AudioPlayer.PlaySound(deathSound);
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
            var _currencyManager = FindObjectOfType<CurrencyManager>();
            _currencyManager?.AddReward(reward);
            
            Destroy(gameObject);
        }
            
    }
}