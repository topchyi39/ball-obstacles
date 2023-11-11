using _Project.Scripts.GameContext;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace _Project.Scripts.Editor
{
    [CustomEditor(typeof(GameContext.GameContext))]
    public sealed class GameContextEditor : UnityEditor.Editor
    {
        private GameContext.GameContext gameContext;

        private void OnEnable()
        {
            gameContext = target as GameContext.GameContext;
        }

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.EnumPopup("State:", gameContext.GameState);
            GUI.enabled = true;

            EditorGUILayout.Space(12);
            if (GUILayout.Button("Start Game"))
            {
                gameContext.StartGame();
            }

            if (GUILayout.Button("Pause Game"))
            {
                gameContext.PauseGame();
            }
            
            if (GUILayout.Button("Resume Game"))
            {
                gameContext.ResumeGame();
            }

            if (GUILayout.Button("Victory Game"))
            {
                gameContext.FinishGame(GameResult.Victory);
            }

            if (GUILayout.Button("Defeat Game"))
            {
                gameContext.FinishGame(GameResult.Defeat);
            }
        }
    }
}
#endif