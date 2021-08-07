using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collision with {other.gameObject.name}");
        if (other.gameObject.GetComponent<Snowman>())
            gameEvents.gameOver.Invoke();
    }
}
