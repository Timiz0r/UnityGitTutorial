using UnityEditor;

namespace TimiUtils.GitTutorial
{
    public class GitTutorialWindow : EditorWindow
    {
        private GitTutorialSelector tutorialSelector;

        [MenuItem("Window/Git Tutorial")]
        public static void Open()
        {
            EditorWindow.GetWindow<GitTutorialWindow>("Git Tutorial").Show();
        }

        public void Awake()
        {
            tutorialSelector = new GitTutorialSelector();
        }

        public void OnGUI()
        {
            tutorialSelector = tutorialSelector ?? new GitTutorialSelector();
            if (tutorialSelector.SelectedTutorial == null)
            {
                tutorialSelector.Render();
            }
            if (tutorialSelector.SelectedTutorial == null) return;

            tutorialSelector.SelectedTutorial.Render();
        }
    }
}