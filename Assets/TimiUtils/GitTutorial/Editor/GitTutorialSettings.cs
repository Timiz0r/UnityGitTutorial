using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace TimiUtils.GitTutorial
{
    public class GitTutorialSettings
    {
        public static readonly RuntimePlatform[] SupportedPlatforms = new[] { RuntimePlatform.WindowsEditor };
        private LocalizationSettings localizationSettings;
        private readonly StringTable stringTable;
        private readonly AssetTable assetTable;
        private readonly RuntimePlatform platform;
        private readonly string prefix = string.Empty;

        public GitTutorialSettings(
            LocalizationSettings localizationSettings,
            RuntimePlatform platform
        )
        {
            if (!SupportedPlatforms.Contains(platform)) throw new ArgumentOutOfRangeException(nameof(platform));

            this.platform = platform;
            this.localizationSettings = localizationSettings ?? throw new ArgumentNullException();
            //Debug.Log(string.Join(";", localizationSettings.GetStringDatabase().GetAllTables().WaitForCompletion().Select(t => t.TableCollectionName)));

            stringTable = localizationSettings.GetStringDatabase().GetTable($"GitTutorial-Strings-{platform}") ?? throw new ArgumentOutOfRangeException();
            assetTable = localizationSettings.GetAssetDatabase().GetTable($"GitTutorial-Assets-{platform}") ?? throw new ArgumentOutOfRangeException();
        }

        public GitTutorialSettings(
            LocalizationSettings localizationSettings,
            RuntimePlatform platform,
            string prefix
        ) : this(localizationSettings, platform)
        {
            this.prefix = prefix;
        }

        public GitTutorialSettings GetContext(string prefix)
            => new GitTutorialSettings(localizationSettings, platform, prefix);

        public string GetString(string key)
            => stringTable[key]?.LocalizedValue ?? throw new ArgumentOutOfRangeException(nameof(key), $"There is no value for key '{key}'.");

        public string GetString(string key, params object[] args)
            => stringTable[key]?.GetLocalizedString(args) ?? throw new ArgumentOutOfRangeException(nameof(key), $"There is no value for key '{key}'.");
    }
}