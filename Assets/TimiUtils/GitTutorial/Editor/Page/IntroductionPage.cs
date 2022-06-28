using UnityEditor;
using UnityEngine;

namespace TimiUtils.GitTutorial
{
    public class IntroductionPage : IGitTutorialPage
    {
        private GitTutorialSettings settings;

        public IntroductionPage(GitTutorialSettings settings)
        {
            this.settings = settings.GetContext("GitTutorial.Page.Introduction");
        }

        public string Name => settings.GetString("PageName");

        public void Render()
        {
            EditorGUILayout.LabelField(settings.GetString("Label1"), new GUIStyle(EditorStyles.label)
            {
                wordWrap = true
            });
        }
    }
}