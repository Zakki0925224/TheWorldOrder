using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImGuiNET;


namespace UnityScripts
{
    public class Gui : MonoBehaviour
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
            ImGui.ShowDemoWindow();
        }
    }
}
