using UnityEngine;

public class SnowmanDeath : MonoBehaviour
{
    [SerializeField] private AudioClipSO deathSound;
    
    private void Start()
    {
        AudioClipSO.Play(deathSound);
        Destroy(gameObject,6);
    }
}
