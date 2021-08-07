using System.Collections;
using UnityEngine;

public class SnowmanSpawner : MonoBehaviour
{
    public GameObject snowmanPrefab;
    public float delay;
    public float spawnRate;

    private void Start()
    {
        StartCoroutine(SpawnSnowmen());
    }

    private IEnumerator SpawnSnowmen()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            var snowman = Instantiate(snowmanPrefab);
            snowman.transform.position = transform.position;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
