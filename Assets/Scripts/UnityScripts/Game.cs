using UnityEngine;
using UnityEngine.UI;
using Map;

namespace UnityScripts
{
    public class Game : MonoBehaviour
    {
        public string PlayerCountryId { get; set; } = "JPN";
        public MapManager MapManager { get; set; }

        void Start()
        {
            // set map loader
            this.MapManager = MapLoader.Load();
            this.MapManager.SetRandomColorByCountry();
            Debug.Log("Set map loader");

            UpdateUI();
        }

        void Update()
        {

        }

        private void UpdateUI()
        {
            var playerCountry = this.MapManager.FindCountryById(this.PlayerCountryId);

            var uiCanvas = GameObject.Find("UICanvas");

            // status panel
            var statusPanel = uiCanvas.transform.Find("StatusPanel");
            var flag = statusPanel.transform.Find("Flag").GetComponent<Image>();
            flag.sprite = playerCountry.FlagSprite;
        }
    }
}
