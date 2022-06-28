using System;
using UnityEditor;
using UnityEngine;

namespace TimiUtils.GitTutorial
{
    public class GitTutorial
    {
        private readonly GitTutorialSettings settings;

        public GitTutorial(GitTutorialSettings settings)
        {
            this.settings = settings;
        }

        internal void Render()
        {
            EditorGUILayout.LabelField("TODO");
        }
    }
}