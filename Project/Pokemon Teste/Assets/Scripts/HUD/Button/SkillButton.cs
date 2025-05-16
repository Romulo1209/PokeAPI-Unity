using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SkillButton : ButtonBase
{
    [SerializeField] private MoveDataModel move;

    [SerializeField] private TMP_Text moveName;

    private bool hasMove = false;

    [Header("Events")]
    [HideInInspector] public UnityEvent<MoveDataModel> onSkillHoverEnter;
    [HideInInspector] public UnityEvent<MoveDataModel> onSkillHoverExit;
    [HideInInspector] public UnityEvent<MoveDataModel> onSkillClick;

    public void SetupMove(Move move)
    {
        if(move != null) {
            moveName.text = move.GetMoveName.ToUpper();
            StartCoroutine(BattleController.instance.GetAPIController.GetMoveData(move.url, (data) => {
                this.move = data;
                hasMove = true;
            }));
        }
        else {
            moveName.text = "-";
            hasMove = false;
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (hasMove) {
            onSkillHoverEnter?.Invoke(move);
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (hasMove) {

        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (hasMove) {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
