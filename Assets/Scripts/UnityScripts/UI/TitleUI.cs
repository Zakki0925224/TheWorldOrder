using UnityEngine;
using ImGuiNET;

namespace UnityScripts.UI
{
    public class TitleUI : MonoBehaviour
    {
        private bool MainWindowOpened = true;
        private bool SelectScenarioWindowOpened = false;

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
            this.LayoutMainMenuWindow();
            this.LayoutSelectScenarioWindow();
        }

        private void LayoutMainMenuWindow()
        {
            ImGui.SetNextWindowSize(new Vector2(200, 300));
            var windowFlags = ImGuiWindowFlags.NoCollapse |
                              ImGuiWindowFlags.NoTitleBar |
                              ImGuiWindowFlags.NoResize |
                              ImGuiWindowFlags.NoMove;

            ImGui.Begin("MainMenu", ref this.MainWindowOpened, windowFlags);

            if (this.ButtonCenteredOnLine("Play", 180f))
                this.SelectScenarioWindowOpened = !this.SelectScenarioWindowOpened;

            if (this.ButtonCenteredOnLine("Exit", 180f))
            {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            ImGui.End();
        }

        private void LayoutSelectScenarioWindow()
        {
            ImGui.SetNextWindowPos(new Vector2(200, 300));
            var windowFlags = ImGuiWindowFlags.NoTitleBar |
                              ImGuiWindowFlags.NoResize |
                              ImGuiWindowFlags.NoMove |

            ImGui.Begin("SelectScenario", ref this.SelectScenarioWindowOpened, windowFlags);
            ImGui.Text("hoge");
            ImGui.End();
        }

        private bool ButtonCenteredOnLine(string label, float size, float alignment = 0.5f)
        {
            var avail = ImGui.GetContentRegionAvail().x;

            var offset = (avail - size) * alignment;
            if (offset > 0.0f)
                ImGui.SetCursorPosX(ImGui.GetCursorPosX() + offset);

            return ImGui.Button(label, new Vector2(size, 0.0f));
        }
    }
}
