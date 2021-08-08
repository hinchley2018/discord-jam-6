using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private int waveNumber;
    [SerializeField] private GameEvents gameEvents;
    private readonly List<SnowmanSpawner> _spawners = new List<SnowmanSpawner>();
    private readonly List<Snowman> _snowmen = new List<Snowman>();
    private bool _wasRunning;
    private bool _wereAllSnowmenMelted;
    private Animator _animator;
    private TextMeshProUGUI _textMesh;

    private bool IsRunning => _spawners.Any(x => x.isRunning);
    private bool AllWaveSnowmenMelted => _spawners.All(x => x.AllSnowmenMelted);

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _textMesh.text = $"Wave {waveNumber}";
    }

    private void OnEnable()
    {
        _wasRunning = false;
        _wereAllSnowmenMelted = true;
        _spawners.AddRange(GetComponentsInChildren<SnowmanSpawner>());
        gameEvents.allWaveSnowmenMelted.AddListener(FinishWave);
    }

    private void OnDisable()
    {
        _spawners.Clear();
        _snowmen.Clear();
        gameEvents.allWaveSnowmenMelted.RemoveListener(FinishWave);
    }

    private void Update()
    {
        if (_wasRunning && !IsRunning)
        {
            gameEvents.finishSpawning.Invoke();
            Debug.Log("Wave: Wave Finished Spawning...");
        }

        if (!IsRunning && !_wereAllSnowmenMelted && AllWaveSnowmenMelted)
        {
            gameEvents.allWaveSnowmenMelted.Invoke();
            Debug.Log("Wave: Wave Spawn Completed...");
        }

        _wasRunning = IsRunning;
        _wereAllSnowmenMelted = AllWaveSnowmenMelted;
    }

    private void FinishWave()
    {
        StartCoroutine(FinishWaveCoroutine());
    }

    private IEnumerator FinishWaveCoroutine()
    {
        _textMesh.text = $"Wave {waveNumber} Completed!";
        _animator.Play("Wave_Canvas_Animation", -1, 0);
        yield return new WaitForSeconds(5);
        gameEvents.startWave.Invoke(waveNumber + 1);
    }
}
