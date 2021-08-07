using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private int health;
    private float damageDelay = 1;
    private List<GameObject> snowmanInRange = new List<GameObject>();
    private Coroutine coroutine;
    private void Start()
    {
        StartCoroutine(CheckForDamage());
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
                        Debug.Log($"Towncenter Takes 1 damage from snowman at {i}, remaining health: {health}");
                        health -= 1;
                    }
                    else
                    {
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
