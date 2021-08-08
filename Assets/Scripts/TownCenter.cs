using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownCenter : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private AudioClipSO damageSound;
    private float damageDelay = 1;
    private List<GameObject> snowmanInRange = new List<GameObject>();
    private Coroutine coroutine;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        StartCoroutine(CheckForDamage());
    }

    private void Update()
    {
        _slider.value = (float)health / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision with {other.gameObject.name}");
        if (other.gameObject.GetComponent<Snowman>())
        {
            snowmanInRange.Add(other.gameObject);
            
        }
            
    }
    
    private IEnumerator CheckForDamage()
    {
        while (health > 0)
        {
            for (int i = 0; i < snowmanInRange.Count; i++)
            {
                var snowman = snowmanInRange[i];
                if (snowman != null && snowman.activeInHierarchy)
                {
                    if (health > 0)
                    {
                        AudioClipSO.Play(damageSound);
                        Debug.Log($"Towncenter Takes 1 damage from snowman at {i}, remaining health: {health}");
                        health -= 1;
                    }
                    else
                    {
                        gameEvents.gameOver.Invoke();
                        break;
                    }
                }
                else
                {
                    Debug.Log($"Snowman no longer active removing at {i}");
                    snowmanInRange.RemoveAt(i);
                }
            }
            
            
            yield return new WaitForSeconds(damageDelay);
        }
        if (health == 0)
        {
            gameEvents.gameOver.Invoke();
        }
    }
}
