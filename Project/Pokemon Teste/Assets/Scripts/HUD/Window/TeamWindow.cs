using UnityEngine;


public class TeamWindow : WindowBase
{
    [SerializeField] private PartyPokemonButton[] partyPokemonButtons;

    private void OnEnable()
    {
        foreach(PartyPokemonButton button in partyPokemonButtons) {
            button.onPokemonPartyClick.AddListener(SetupParty);
        }
    }
    private void OnDisable()
    {
        foreach (PartyPokemonButton button in partyPokemonButtons) {
            button.onPokemonPartyClick.RemoveListener(SetupParty);
        }
    }

    public override void OpenWindow()
    {
        base.OpenWindow();
        SetupParty(BattleController.instance.GetPlayerBelt);
    }

    public void SetupParty(PokemonBelt belt)
    {
        var activePokemon = belt.ActivePokemon;

        partyPokemonButtons[0].SetupPartyPokemonHUD(activePokemon);

        int buttonIndex = 1;
        for (int i = 0; i < belt.GetPokemons.Count; i++)
        {
            var pokemon = belt.GetPokemons[i];
            if (pokemon != null && pokemon != activePokemon)
            {
                if (buttonIndex < partyPokemonButtons.Length)
                {
                    partyPokemonButtons[buttonIndex].SetupPartyPokemonHUD(pokemon);
                    buttonIndex++;
                }
            }
        }

        for (int i = buttonIndex; i < partyPokemonButtons.Length; i++)
        {
           
        }
    }
}