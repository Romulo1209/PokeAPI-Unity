using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummaryWindow : WindowBase
{
    [SerializeField] private TMP_Text pokemonIdText;
    [SerializeField] private TMP_Text pokemonNameText;
    [SerializeField] private TMP_Text pokemonLevelText;
    [SerializeField] private TMP_Text pokemonNumberRandomText;
    [SerializeField] private Image pokemonImage;
    public override void OpenWindow(Pokemon pokemon)
    {
        base.OpenWindow();
        SetupSummary(pokemon);
    }

    private void SetupSummary(Pokemon pokemon)
    {
        pokemonIdText.text = $"{pokemon.GetPokemonID}";
        pokemonNameText.text = $"{char.ToUpper(pokemon.GetPokemonName[0])}{pokemon.GetPokemonName.Substring(1)}";
        pokemonLevelText.text = $"{pokemon.GetPokemonLevel}";
        pokemonNumberRandomText.text = $"{Random.Range(0,10000)}";
        pokemonImage.sprite = pokemon.GetPokemonSprites[1];
    }
}
