using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer singleton;

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (singleton == this)
            singleton = null;
    }

    public static void PlaySound(AudioClip clip)
    {
        var audioGameObject = new GameObject(clip.name);
        var source = audioGameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        Destroy(audioGameObject, clip.length);
    }
}