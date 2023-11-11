using System.Collections;
using _Project.Scripts.ObjectPooling;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Scripts.World.SFX
{
    public interface ISfx
    {
        float Volume { get; }
        float Pitch { get; }
        
        ISfx SetVolume(float volume);
        ISfx SetVolumeFade(float volume);
        ISfx SetPitch(float pitch);
        ISfx SetGroup(AudioMixerGroup mixerGroup);
        void Stop();
    }
    
    public class PooledSfx : PooledObject, ISfx
    {

        public float Volume => source.volume;
        public float Pitch => source.pitch;
        
        [SerializeField] private AudioSource source;
        
        public void Init(AudioClip clip, float defaultVolume, float defaultPitch, bool loop)
        {
            source.loop = loop;
            source.clip = clip;
            source.volume = defaultVolume;
            source.pitch = defaultPitch;
        }

        public void Play()
        {
            source.Play();
            if (!source.loop) StartCoroutine(PlayRoutine());
        }

        public ISfx SetVolume(float volume)
        {
            source.volume = volume;
            return this;
        }

        public ISfx SetVolumeFade(float volume)
        {
            source.DOFade(volume, 1f);
            return this;
        }

        public ISfx SetPitch(float pitch)
        {
            source.pitch = Mathf.Clamp(pitch, 0f, 2f);
            return this;
        }

        public ISfx SetGroup(AudioMixerGroup mixerGroup)
        {
            source.outputAudioMixerGroup = mixerGroup;
            return this;
        }

        public void Stop()
        {
            source.Stop();
            ReturnToPool();
        }

        private IEnumerator PlayRoutine()
        {
            yield return new WaitForSeconds(source.clip.length);
            ReturnToPool();
        }
    }
}