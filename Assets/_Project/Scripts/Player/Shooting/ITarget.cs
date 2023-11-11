using System;

namespace _Project.Scripts.Player.Shooting
{
    public interface ITarget
    {
        void Hit(float radius);
        event Action Destroyed;
    }
}