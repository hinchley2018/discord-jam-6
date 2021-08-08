using UnityEngine;

public class NiceSnowman : MonoBehaviour
{
    public GameObject snowmanPrefab;

    public void Convert()
    {
        Instantiate(snowmanPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
