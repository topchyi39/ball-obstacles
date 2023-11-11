using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public interface IPlayerRadiusUpdater
    {
        float CriticalValue { get; }
        float DefaultValue { get; }
        event Action<float> ValueChanged;
    }
    
    public class PlayerRadiusPercentage : MonoBehaviour
    {
        [SerializeField] private TMP_Text textTMP;
        [SerializeField] private Color goodColor = Color.green;
        [SerializeField] private Color criticalColor = Color.red;
        [SerializeField] private string format = "{0}%";
        
        private float _criticalValue;
        private float _defaultValue;
        
        public void Construct(IPlayerRadiusUpdater playerRadiusNotifier)
        {
            _criticalValue = playerRadiusNotifier.CriticalValue;
            _defaultValue = playerRadiusNotifier.DefaultValue;
            playerRadiusNotifier.ValueChanged += UpdateRadiusPercentage;
        }

        private void UpdateRadiusPercentage(float value)
        {
            var percentage = value / _defaultValue * 100f;
            
            var t = Mathf.InverseLerp(_defaultValue, _criticalValue, value);
            textTMP.color = Color.Lerp(goodColor, criticalColor, t);
            textTMP.text = string.Format(format, percentage.ToString("F1"));
        }
    }
}