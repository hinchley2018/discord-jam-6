using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main _singleton;
    public AudioClipSO happyMusic;
    public AudioClipSO angryMusic;
    private AudioSource _currentMusic;
    private static bool isHappyMusic;

    [RuntimeInitializeOnLoadMethod]
    public static void CreateInstance()
    {
        if (_singleton) return;
        var newGameObject = (GameObject)Instantiate(Resources.Load("Main"));
        DontDestroyOnLoad(newGameObject);
        _singleton = newGameObject.GetComponent<Main>();
    }

    public static void PlayAngryMusic()
    {
        if (!isHappyMusic) return;
        if (!_singleton) return;
        if (_singleton._currentMusic)
            _singleton._currentMusic.Stop();
        _singleton._currentMusic = AudioClipSO.Play(_singleton.angryMusic);
        _singleton._currentMusic.transform.SetParent(_singleton.transform);
        isHappyMusic = false;
    }
    
    public static void PlayHappyMusic()
    {
        if (isHappyMusic) return;
        if (!_singleton) return;
        if (_singleton._currentMusic)
            _singleton._currentMusic.Stop();
        _singleton._currentMusic = AudioClipSO.Play(_singleton.happyMusic);
        _singleton._currentMusic.transform.SetParent(_singleton.transform);
        isHappyMusic = true;
    }

    private void Start()
    {
        isHappyMusic = false;
        PlayHappyMusic();
    }
}
