using System.Collections.Generic;
using _Project.Scripts.GameContext;
using _Project.Scripts.ObjectPooling;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.UI;

namespace _Project.Scripts.World.SFX
{
    public class SfxController : MonoBehaviour, IStartGameListener, IFinishGameListener
    {
        [SerializeField] private PooledSfx sfxPrefab;
        [SerializeField] private SfxClipsLibrary clipsLibrary;
        [SerializeField] private AudioMixerGroup defaultGroup;
        [SerializeField] private AudioMixerGroup musicGroup;
        
        private ObjectPool _pool;

        private ISfx _background;
        
        private Dictionary<SfxType, AudioClip> _clips;

        private const int DefaultCapacity = 5;
        private const int MaxCapacity = 5;
        private const float DefaultVolume = 1f;
        private const float DefaultPitch = 1f;
        
        public void Construct()
        {
            _pool = new ObjectPool(sfxPrefab, true, DefaultCapacity, MaxCapacity);
            _clips = clipsLibrary.GetClips();
            _background = PlaySfx(clipsLibrary.GetBackgroundClip(), true).SetGroup(musicGroup);
        }

        public ISfx PlaySfx(SfxType type, bool loop = false)
        {
            if (!_clips.ContainsKey(type)) return null;
            
            var pooledSfx = _pool.Get() as PooledSfx;

            if (pooledSfx == null) return null;
            
            pooledSfx.Init(_clips[type], DefaultVolume, DefaultPitch, loop);
            pooledSfx.Play();
            pooledSfx.SetGroup(defaultGroup);
            return pooledSfx;
        }
        
        public ISfx PlaySfx(AudioClip clip, bool loop = false)
        {
            var pooledSfx = _pool.Get() as PooledSfx;

            if (pooledSfx == null) return null;
            
            pooledSfx.Init(clip, DefaultVolume, DefaultPitch, loop);
            pooledSfx.Play();

            return pooledSfx;
        }

        public void OnStartGame()
        {
            _background.SetVolumeFade(DefaultVolume * 0.5f);
        }

        public void OnFinishGame()
        {
            _background.SetVolumeFade(DefaultVolume);
        }
    }
}