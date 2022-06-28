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
            var context = settings.GetContext("Tutorial");

            EditorGUILayout.BeginHorizontal();
            currentPageIndex = EditorGUILayout.Popup(
                context.GetString("JumpLabel"),
                currentPageIndex,
                pages.Select(p => p.Name).ToArray());
            if (GUILayout.Button(context.GetString("ResetLabel")))
            {
                finished = true;
                return;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(currentPageIndex == 0);
            if (GUILayout.Button(context.GetString("PreviousPage")))
            {
                currentPageIndex--;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(currentPageIndex + 1 >= pages.Count);
            if (GUILayout.Button(context.GetString("NextPage")))
            {
                currentPageIndex++;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            var currentPage = pages[currentPageIndex];
            EditorGUILayout.LabelField(currentPage.Name, new GUIStyle(EditorStyles.largeLabel)
            {
                fontSize = 28,
                wordWrap = true
            });

            EditorGUILayout.Space();

            currentPage.Render();
        }
    }
}