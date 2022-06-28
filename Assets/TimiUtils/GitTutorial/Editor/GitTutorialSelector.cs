using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace TimiUtils.GitTutorial
{
    public class GitTutorialSelector
    {
        private readonly LocalizationSettings localizationSettings;
        private int selectedLocaleIndex = 0;
        private int selectedPlatformIndex = 0;
        //in case we want to use localization in the selector, we'll populate this a bit early
        private GitTutorialSettings settings = null;
        public GitTutorial SelectedTutorial { get; private set; }

        public GitTutorialSelector()
        {
            localizationSettings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>("Assets/Locales/GitTutorialLocalizationSettings.asset");
            localizationSettings.GetInitializationOperation().WaitForCompletion();

            selectedPlatformIndex = ArrayUtility.FindIndex(GitTutorialSettings.SupportedPlatforms, p => p == Application.platform);
            if (selectedPlatformIndex == -1)
            {
                selectedPlatformIndex = 0;
                Debug.LogWarning($"Could not find a supported tutorial platform for the current platform. Using '{GitTutorialSettings.SupportedPlatforms[selectedPlatformIndex]}'.");
            }

            var defaultLocale = localizationSettings.GetStartupLocaleSelectors()[0].GetStartupLocale(localizationSettings.GetAvailableLocales());
            SetLocale(defaultLocale);
        }
    
        public void Render()
        {
            //for whatever reason on domain reload or whatever, the asset seems to uninitialize
            localizationSettings.GetInitializationOperation().WaitForCompletion();

            EditorGUILayout.LabelField("Language");
            var availableLocales = localizationSettings.GetAvailableLocales().Locales;
            var newLocaleIndex = EditorGUILayout.Popup(selectedLocaleIndex, availableLocales.Select(l => l.LocaleName).ToArray());
            if (newLocaleIndex != selectedLocaleIndex)
            {
                selectedLocaleIndex = newLocaleIndex;
                SetLocale(availableLocales[selectedLocaleIndex]);
            }

            var context = settings.GetContext("TutorialSelector");

            EditorGUILayout.LabelField(context.GetString("PlatformLabel"));
            var newPlatformIndex = EditorGUILayout.Popup(
                selectedPlatformIndex,
                GitTutorialSettings.SupportedPlatforms
                    .Select(p => p.ToString().Replace("Editor", string.Empty))
                    .ToArray());
            if (newPlatformIndex != selectedPlatformIndex)
            {
                selectedPlatformIndex = newPlatformIndex;
            }

            if (GUILayout.Button(context.GetString("StartButton")))
            {
                SelectedTutorial = new GitTutorial(settings);
            }
        }

        private void SetLocale(Locale locale)
        {
            localizationSettings.SetSelectedLocale(locale);
            settings = new GitTutorialSettings(localizationSettings, GitTutorialSettings.SupportedPlatforms[selectedPlatformIndex]);
        }
    }
}