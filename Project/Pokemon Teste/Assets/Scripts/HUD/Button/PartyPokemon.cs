using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PartyPokemonButton : ButtonBase
{
    private Pokemon pokemon;

    [SerializeField] private TMP_Text pokemonNameText;
    [SerializeField] private TMP_Text pokemonLevelText;
    [SerializeField] private TMP_Text pokemonLifeText;
    [SerializeField] private Image pokemonMiniatureImage;
    [SerializeField] private Image pokemonLifeSlider;

    [HideInInspector] public UnityEvent<PokemonBelt> onPokemonPartyClick;

    public void SetupPartyPokemonHUD(Pokemon pokemon) {
        this.pokemon = pokemon;

        int pokemonLevel = pokemon.GetPokemonLevel;
        pokemonNameText.text = char.ToUpper(pokemon.GetPokemonName[0]) + pokemon.GetPokemonName.Substring(1);
        pokemonLevelText.text = $"Lv{pokemon.GetPokemonLevel}";
        pokemonLifeText.text = $"{pokemon.GetPokemonActualLife}/{pokemon.GetPokemonMaxLife}";
        pokemonLifeSlider.fillAmount = pokemon.GetPokemonActualLife / pokemon.GetPokemonMaxLife;
        pokemonMiniatureImage.sprite = pokemon.GetPokemonSprites[1];
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if(pokemon != null) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                BattleController.instance.GetPlayerBelt.ActivePokemon = pokemon;
                onPokemonPartyClick?.Invoke(BattleController.instance.GetPlayerBelt);
            }
            else if (eventData.button == PointerEventData.InputButton.Right) {
                BattleController.instance.GetHUDController.OpenWindow(3, pokemon);
            }
        }
    }
}
