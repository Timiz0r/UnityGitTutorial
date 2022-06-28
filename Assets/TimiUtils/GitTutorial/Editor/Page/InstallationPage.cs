using UnityEngine;

namespace TimiUtils.GitTutorial
{
    public class InstallationPage : IGitTutorialPage
    {
        private GitTutorialSettings settings;

        public InstallationPage(GitTutorialSettings settings)
        {
            this.settings = settings.GetContext("GitTutorial.Page.Installation");
        }

        public string Name => settings.GetString("PageName");

        public void Render()
        {
            throw new System.NotImplementedException();
        }
    }
}