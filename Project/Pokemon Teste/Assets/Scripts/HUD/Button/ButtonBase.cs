using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Events")]
    [HideInInspector] public UnityEvent onHoverEnter;
    [HideInInspector] public UnityEvent onHoverExit;
    [HideInInspector] public UnityEvent onClick;


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEnter?.Invoke();
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit?.Invoke();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
