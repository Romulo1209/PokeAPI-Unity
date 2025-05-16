using UnityEngine;

public class BattleMenuWindow : WindowBase
{
    [Header("Battle Menu Window")]
    [SerializeField] private BattleMenuButton[] batttleMenuButtons;

    public override void OpenWindow()
    {
        base.OpenWindow();
    }

    private void OnEnable()
    {
        foreach (BattleMenuButton button in batttleMenuButtons) {
            button.onBattleMenuButtonClick.AddListener(BattleController.instance.GetHUDController.OpenWindow);
        }
    }

    private void OnDisable()
    {
        foreach (BattleMenuButton button in batttleMenuButtons) {
            button.onBattleMenuButtonClick.RemoveListener(BattleController.instance.GetHUDController.OpenWindow);
        }
    }
}
