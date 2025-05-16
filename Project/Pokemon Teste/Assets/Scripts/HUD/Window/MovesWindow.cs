using TMPro;
using UnityEngine;

public class MovesWindow : WindowBase
{
    [Header("Moves Window")]
    [SerializeField] private SkillButton[] moveButtons;

    [SerializeField] private TMP_Text moveTypeText;
    [SerializeField] private TMP_Text movePPText;

    private void OnEnable()
    {
        foreach (SkillButton button in moveButtons) {
            button.onSkillHoverEnter.AddListener(ShowMoveInformations);
        }
    }

    private void OnDisable()
    {
        foreach (SkillButton button in moveButtons) {
            button.onSkillHoverEnter.RemoveListener(ShowMoveInformations);
        }
    }


    public override void OpenWindow()
    {
        base.OpenWindow();
        SetupSkills(BattleController.instance.GetPlayerPokemonDataModel);
    }

    void SetupSkills(Pokemon pokemon) 
    {
        int i = 0;
        foreach (SkillButton button in moveButtons)
        {
            Move move = pokemon.GetPokemonMoves[i];
            button.SetupMove(move);
            i++;
        }
    }

    void ShowMoveInformations(MoveDataModel move)
    {
        moveTypeText.text = move.GetMoveType.ToUpper();
        movePPText.text = $"{move.GetMovePP}/{move.GetMovePP}";
    }
}
