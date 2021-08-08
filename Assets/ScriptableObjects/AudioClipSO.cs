using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObject/AudioClipSO")]
    public class AudioClipSO : ScriptableObject
    {
        public AudioClip clip;
        public AudioMixerGroup outputAudioMixerGroup;
        [Range(0, 1)] public float volume = 1;
        [Range(-3, 3)] public float pitch = 1;
        [Range(0, 1)] public float spatialBlend2Dto3D;
        public float minDistance;
        public float maxDistance = 45;
        public bool loop;
        public AudioRolloffMode audioRolloff = AudioRolloffMode.Linear;
        public bool playOnAwake;
        public float start;
        public float stop = -1;
        public float duration = -1;

        public static AudioSource Play(AudioClipSO audioClipSo) => Play(audioClipSo, Vector3.zero);

        public static AudioSource Play(AudioClipSO audioClipSo, Vector3 position)
        {
            if (audioClipSo)
                return audioClipSo.Play(position);
            
            if (!audioClipSo)
                Debug.LogWarning("Attempting to play {NULL} Audio");
            return null;
        }
        
        public static AudioSource Play(AudioClipSO audioClipSo, Transform transform)
        {
            if (audioClipSo && transform)
                return audioClipSo.Play(transform.position);
            
            if (!audioClipSo)
                Debug.LogWarning("Attempting to play {NULL} Audio");
            else if (!transform)
                Debug.LogWarning($"Attempting to play {audioClipSo.name} at {{NULL}} transform");
            return null;
        }

        public static AudioSource Loop(AudioClipSO audioClipSo, Vector3 position)
        {
            if (audioClipSo)
                return audioClipSo.Loop(position);
            
            if (!audioClipSo)
                Debug.LogWarning("Attempting to loop {NULL} Audio");
            return null;
        }

        protected virtual AudioSource Play() => Play(Vector3.zero);

        protected virtual AudioSource Play(Vector3 position)
        {
            var gameObject = new GameObject(clip.name);
            var audioSource = gameObject.AddComponent<AudioSource>();
            gameObject.transform.position = position;
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.loop = loop;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.spatialBlend = spatialBlend2Dto3D;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.rolloffMode = audioRolloff;

            var mb = gameObject.AddComponent<CoroutineMonoBehaviour>();
            if (stop > 0)
                mb.StartCoroutine(Play(audioSource, start, stop));
            else if (duration > 0)
                mb.StartCoroutine(PlayForDuration(audioSource, start, duration));
            else
                mb.StartCoroutine(PlayForDuration(audioSource, start));

            return audioSource;
        }

        protected virtual AudioSource Loop(Vector3 position)
        {
            var gameObject = new GameObject(clip.name);
            var audioSource = gameObject.AddComponent<AudioSource>();
            gameObject.transform.position = position;
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop = true;
            audioSource.spatialBlend = spatialBlend2Dto3D;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.rolloffMode = audioRolloff;
            return audioSource;
        }

        private static IEnumerator Play(AudioSource audioSource, float startTime = 0, float stopTime = -1)
        {
            var duration = stopTime > 0 ? stopTime - startTime : audioSource.clip.length - startTime;
            yield return PlayForDuration(audioSource, startTime, duration);
        }

        private static IEnumerator PlayForDuration(AudioSource audioSource, float startTime = 0,
            float clipDuration = -1)
        {
            audioSource.time = startTime;
            audioSource.Play();
            clipDuration = clipDuration > 0 ? clipDuration : audioSource.clip.length - startTime;
            yield return new WaitForSeconds(clipDuration);
            audioSource.Stop();
            Destroy(audioSource.gameObject);
        }

        private class CoroutineMonoBehaviour : MonoBehaviour
        {
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AudioClipSO)), UnityEditor.CanEditMultipleObjects]
    public class AudioClipEditor : UnityEditor.Editor
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
                foreach (var audioClipTarget in targets)
                {
                    AudioClipSO audioClipSo = (AudioClipSO) audioClipTarget;
                    audioSource.clip = audioClipSo.clip;
                    audioSource.pitch = audioClipSo.pitch;
                    audioSource.volume = audioClipSo.volume;
                    audioSource.time = audioClipSo.start;
                    startTime = Time.realtimeSinceStartup;
                    endTime = startTime + audioSource.clip.length;
                    endTime = audioClipSo.stop > 0 ? startTime + (audioClipSo.stop - audioClipSo.start) : endTime;
                    endTime = audioClipSo.duration > 0 ? startTime + audioClipSo.duration : endTime;
                    audioSource.Play();
                }
            }

            if (GUILayout.Button("Stop Sound"))
            {
                audioSource.Stop();
            }
        }
    }
#endif
