using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TimiUtils.GitTutorial
{
    public class GitTutorial
    {
        private readonly GitTutorialSettings settings;
        private int currentPageIndex = 0;
        private readonly IReadOnlyList<IGitTutorialPage> pages;

        public GitTutorial(GitTutorialSettings settings)
        {
            this.settings = settings;
            pages = new IGitTutorialPage[]
            {
                new IntroductionPage(settings),
                new InstallationPage(settings),
            };
        }

        internal void Render(out bool finished)
        {
            finished = false;
            var tutorialContext = settings.GetContext("Tutorial");

            EditorGUILayout.BeginHorizontal();
            currentPageIndex = EditorGUILayout.Popup(
                tutorialContext.GetString("JumpLabel"),
                currentPageIndex,
                pages.Select(p => p.Name).ToArray());
            if (GUILayout.Button(tutorialContext.GetString("ResetLabel")))
            {
                finished = true;
                return;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(currentPageIndex == 0);
            if (GUILayout.Button(tutorialContext.GetString("PreviousPage")))
            {
                currentPageIndex--;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(currentPageIndex + 1 >= pages.Count);
            if (GUILayout.Button(tutorialContext.GetString("NextPage")))
            {
                currentPageIndex++;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}