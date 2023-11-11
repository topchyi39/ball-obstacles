using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class FrameCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format;
        
        private void Start()
        {
            UpdateFrame();
        }

        private void Update()
        {
            if (Time.frameCount % 3 == 0) UpdateFrame();
        }

        private void UpdateFrame()
        {
            text.text = string.Format(format, (1f / Time.deltaTime).ToString("F1"));
        }
    }
}