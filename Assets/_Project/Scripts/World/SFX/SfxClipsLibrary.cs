using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.World.SFX
{
    [CreateAssetMenu(fileName = "SFX Clips", menuName = "Project/SFX/Clips")]
    public class SfxClipsLibrary : ScriptableObject
    {
        [SerializeField] private AudioClip[] backgroundClips;
        [SerializeField] private SfxClip[] clips;

        private void OnValidate()
        {
            for (var index = 0; index < clips.Length; index++)
            {
                var sfxClip = clips[index];
                if (sfxClip.SfxType == SfxType.None) continue;

                for (var i = 0; i < clips.Length; i++)
                {
                    var secondClip = clips[i];
                    if (sfxClip == secondClip) continue;
                    if (sfxClip.SfxType == secondClip.SfxType) Debug.LogError($"Duplicate SFX Type: {sfxClip.SfxType} at index - {index} and {i}");
                }
            }
        }

        public AudioClip GetBackgroundClip()
        {
            var index = 0;
            if (backgroundClips.Length > 1)
            {
                index = Random.Range(0, backgroundClips.Length);
            }
            
            return backgroundClips[index];
            
        }

        public Dictionary<SfxType, AudioClip> GetClips()
        {
            var dictionary = new Dictionary<SfxType, AudioClip>();
            
            foreach (var sfxClip in clips)
            {
                if (dictionary.ContainsKey(sfxClip.SfxType)) continue;
                
                dictionary.Add(sfxClip.SfxType, sfxClip.Clip);
            }

            return dictionary;
        }
    }
}