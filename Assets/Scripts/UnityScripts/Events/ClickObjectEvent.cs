using UnityEngine;
using UnityEngine.EventSystems;

public class ClickObjectEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Object {this.name} was clicked");
    }
}
