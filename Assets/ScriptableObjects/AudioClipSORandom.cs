using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AudioClipSORandom")]
    public class AudioClipSORandom : AudioClipSO
    {
        public List<AudioClipSO> audioClips = new List<AudioClipSO>();

        protected override AudioSource Play()
        {
            var audioClip = audioClips[Random.Range(0, audioClips.Count)];
            var audioSource = Play(audioClip);
            audioSource.name = audioClip.name;
            return audioSource;
        }
        
        protected override AudioSource Play(Vector3 position)
        {
            var audioClip = audioClips[Random.Range(0, audioClips.Count)];
            var audioSource = Play(audioClip, position);
            audioSource.name = audioClip.name;
            return audioSource;
        }

        public AudioClipSO Select() => audioClips[Random.Range(0, audioClips.Count)];
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AudioClipSORandom))]
    public class AudioClipRandomEditor : UnityEditor.Editor
    {
        public GameObject gameObject;
        public AudioSource audioSource;
        public float startTime;
        public float endTime;

        private void OnEnable()
        {
            UnityEditor.EditorApplication.update += OnEditorUpdate;
        }

        private void OnDisable()
        {
            UnityEditor.EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (audioSource && audioSource.isPlaying && Time.realtimeSinceStartup > endTime)
                audioSource.Stop();
        }

        public override void OnInspectorGUI()
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            base.OnInspectorGUI();

            if (!gameObject)
            {
                gameObject = new GameObject("AudioClip") {hideFlags = HideFlags.HideAndDontSave};
            }

            if (!audioSource)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            if (GUILayout.Button("Play Sound"))
            {
                AudioClipSORandom audioClipSoRandom = (AudioClipSORandom)target;
                AudioClipSO audioClipObject = audioClipSoRandom.Select();
                audioSource.clip = audioClipObject.clip;
                audioSource.pitch = audioClipObject.pitch;
                audioSource.volume = audioClipObject.volume;
                audioSource.time = audioClipObject.start;
                startTime = Time.realtimeSinceStartup;
                endTime = startTime + audioSource.clip.length;
                endTime = audioClipObject.stop > 0 ? startTime + (audioClipObject.stop - audioClipObject.start) : endTime;
                endTime = audioClipObject.duration > 0 ? startTime + audioClipObject.duration : endTime;
                audioSource.Play();
            }

            if (GUILayout.Button("Stop Sound"))
            {
                audioSource.Stop();
            }
        }
    }
#endif