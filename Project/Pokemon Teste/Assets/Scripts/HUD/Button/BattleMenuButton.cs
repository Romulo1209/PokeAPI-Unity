using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BattleMenuButton : ButtonBase
{
    [SerializeField] private int windowIdToOpen;

    [HideInInspector] public UnityEvent onBattleMenuButtonHoverEnter;
    [HideInInspector] public UnityEvent onBattleMenuButtonHoverExit;
    [HideInInspector] public UnityEvent<int> onBattleMenuButtonClick;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        onBattleMenuButtonClick?.Invoke(windowIdToOpen);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
