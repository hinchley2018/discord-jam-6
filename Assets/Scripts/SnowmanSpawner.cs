using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent<Snowman> onSpawn;
    
    private readonly List<Snowman> _snowmen = new List<Snowman>();
    public bool AllSnowmenMelted => isFinished && _snowmen.Count == 0;

    private void OnEnable()
    {
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
        yield return new WaitForSeconds(delay);
        for (var i = 0; i < spawnAmount; i++)
        {
            var snowmanGameObject = Instantiate(snowmanPrefab);
            snowmanGameObject.transform.position = transform.position;
            var snowman = snowmanGameObject.GetComponent<Snowman>();
            snowman.health = snowmanHealth;
            snowman.maxHealth = snowmanMaxHealth;
            onSpawn.Invoke(snowman);
            _snowmen.Add(snowman);
            yield return new WaitForSeconds(spawnRate);
        }

        isFinished = true;
        isRunning = false;
    }
}
