using System;
using UnityEngine;

namespace _Project.Scripts.World.SFX
{
    [Serializable]
    public class SfxClip
    {
        [field: SerializeField] public SfxType SfxType { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
        
    }
}