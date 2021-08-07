using UnityEngine;

public class SnowmanDeath : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    
    private void Start()
    {
        AudioPlayer.PlaySound(deathSound);
        Destroy(gameObject,6);
    }
}
