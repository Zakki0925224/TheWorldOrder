using UnityEngine;
using ImGuiNET;

namespace UnityScripts.UI
{
    public class GameUI : MonoBehaviour
    {
        private void OnEnable()
        {
            ImGuiUn.Layout += OnLayout;
        }

        private void OnDisable()
        {
            ImGuiUn.Layout -= OnLayout;
        }

        private void OnLayout()
        {
            if (ImGui.Begin("hello"))
            {
                ImGui.TextUnformatted("hello");
            }
            ImGui.End();
        }
    }
}
