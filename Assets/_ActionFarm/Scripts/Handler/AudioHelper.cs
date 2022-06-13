using System.Linq;
using _ActionFarm.Scripts.Utilities;
using UnityEngine;

namespace _ActionFarm.Scripts.Handler
{
    public class AudioHelper : Singleton<AudioHelper>
    {
        [SerializeField] private AudioHolder _audioHolder;
        [SerializeField] private AudioSource _mainSoundsAudioSource;
        [SerializeField] private AudioSource[] _soundsAudioSource;

        public AudioHolder AudioHolder => _audioHolder;

        public static AudioClip GetClip(string clipName) => Instance._audioHolder.GetAudioClipByName(clipName);

        public static void PlaySoundByName(string clipName, float minPinch = 1f, float maxPitch = 1f) =>
            PlaySound(Instance._audioHolder.GetAudioClipByName(clipName), minPinch, maxPitch);

        public static void PlaySound(AudioClip clip, float minPinch = 1f, float maxPitch = 1f)
        {
            if (!Instance.TryGetFreeSoundsSourceOrNull(out AudioSource suitableAudioSource))
                return;

            suitableAudioSource.pitch = Random.Range(minPinch, maxPitch);
            suitableAudioSource.clip = clip;
            suitableAudioSource.Play();
        }

        public static void PlayOneShot(AudioClip clip, float minPinch = 1f, float maxPitch = 1f)
        {
            Instance._mainSoundsAudioSource.pitch = Random.Range(minPinch, maxPitch);
            Instance._mainSoundsAudioSource.PlayOneShot(clip);
        }

        private bool TryGetFreeSoundsSourceOrNull(out AudioSource audioSource)
        {
            audioSource = _soundsAudioSource.FirstOrDefault(x => !x.isPlaying);
            return audioSource != null;
        }
    }
}