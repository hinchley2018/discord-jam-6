using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class SnowmanSpawner : MonoBehaviour
{
    public bool isRunning;
    public bool isFinished;
    [SerializeField] private GameObject snowmanPrefab;
    [SerializeField] private float delay;
    [SerializeField] private float spawnRate;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float snowmanHealth;
    [SerializeField] private float snowmanMaxHealth;
    [SerializeField] private int snowmanAttackDamage = 1;
    [SerializeField] private float snowmanSpeed = 1;
    [SerializeField] private float randomnessRange = 1;
    [SerializeField] private float randomOffset;
    public UnityEvent<Snowman> onSpawn;
    private CircleCollider2D _collider;

    private readonly List<Snowman> _snowmen = new List<Snowman>();
    public bool AllSnowmenMelted => isFinished && _snowmen.Count == 0;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        randomOffset = UnityEngine.Random.Range(-randomnessRange, randomnessRange);
        StartCoroutine(SpawnSnowmen());
    }

    private void Update()
    {
        RemoveSnowmenFromListIfMelted();
    }

    private void RemoveSnowmenFromListIfMelted()
    {
        for (var i = _snowmen.Count - 1; i >= 0; i--)
            if (!_snowmen[i])
                _snowmen.RemoveAt(i);
    }

    private IEnumerator SpawnSnowmen()
    {
        isRunning = true;
        yield return new WaitForSeconds(delay + randomOffset);
        for (var i = 0; i < spawnAmount; i++)
        {
            var snowmanGameObject = Instantiate(snowmanPrefab);
            var randomSpawnPositionOffset = (Vector3)UnityEngine.Random.insideUnitCircle * _collider.radius;
            snowmanGameObject.transform.position = transform.position + randomSpawnPositionOffset;
            var snowman = snowmanGameObject.GetComponent<Snowman>();
            snowman.health = snowmanHealth;
            snowman.maxHealth = snowmanMaxHealth;
            snowman.speed = snowmanSpeed;
            snowman.attackDamage = snowmanAttackDamage;
            onSpawn.Invoke(snowman);
            _snowmen.Add(snowman);
            yield return new WaitForSeconds(spawnRate + randomOffset);
        }

        isFinished = true;
        isRunning = false;
    }
}