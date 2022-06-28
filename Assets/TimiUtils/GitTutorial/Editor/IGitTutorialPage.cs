using UnityEngine;

namespace TimiUtils.GitTutorial
{
    public interface IGitTutorialPage
    {
        string Name { get; }
        void Render();
    }
}