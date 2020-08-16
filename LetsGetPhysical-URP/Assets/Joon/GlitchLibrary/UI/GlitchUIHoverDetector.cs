using UnityEngine.EventSystems;

public class GlitchUIHoverDetector : UIBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isHovering;
    public bool wasHovering;

    void LateUpdate()
    {
        wasHovering = isHovering;
    }

	public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
