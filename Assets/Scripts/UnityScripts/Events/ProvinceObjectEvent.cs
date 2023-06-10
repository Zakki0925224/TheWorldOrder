using UnityEngine;
using UnityEngine.EventSystems;
using ShpLoader;
using UnityScripts;

namespace UnityScripts.Events
{
    public class ProvinceObjectEvent : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var objectRecord = this.GetComponent<ObjectRecord>();
            var map = GameObject.Find("Map").GetComponent<Map>();
            var mapManager = map.Manager;

            var province = mapManager.FindStateByProvinceObjectRecord(objectRecord);
            var state = mapManager.FindStateByProvinceObjectRecord(objectRecord);
            var country = mapManager.FindCountryByProvinceObjectRecord(objectRecord);

            if (province == null)
            {
                Debug.Log($"The province was not defined");
                return;
            }

            if (state == null)
            {
                Debug.Log($"The state was not defined");
                return;
            }

            if (country == null)
            {
                Debug.Log($"The country was not defined");
                return;
            }

            Debug.Log($"Country: {country.Name}, State: {state.Name}, Province id: {province.Id}");
        }
    }
}
