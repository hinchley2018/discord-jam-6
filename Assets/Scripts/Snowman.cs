using System;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    private void Start()
    {
        target = FindObjectOfType<TownCenter>().transform;
    }

    private void Update()
    {
        var direction = target.position - transform.position;
        direction = direction.normalized;
        transform.position += direction * (speed * Time.deltaTime);
    }
}