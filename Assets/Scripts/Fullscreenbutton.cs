using UnityEngine;
using UnityEngine.EventSystems;

public class FullscreenToggleButton : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
