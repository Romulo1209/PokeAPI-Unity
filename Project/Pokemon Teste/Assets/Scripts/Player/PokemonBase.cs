using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controla a HUD e os dados visuais de um Pok�mon no campo de batalha.
/// Atualiza nome, n�vel, sprites e barra de vida com base nos dados fornecidos.
/// </summary>
public class PokemonBase : MonoBehaviour
{
    [Header("Pokemon Model")]
    [SerializeField] private Pokemon pokemon;

    [Header("Pokemon Informations")]
    [SerializeField] private bool enemyPokemon = false;
    [SerializeField] private int pokemonActualLife;
    [SerializeField] private int pokemonMaxLife;

    [Header("HUD Elements")]
    [SerializeField] private Image pokemonSpriteImage;
    [SerializeField] private Image pokemonLifeSlider;
    [SerializeField] private TMP_Text? pokemonLifeText;
    [SerializeField] private TMP_Text pokemonNameText;
    [SerializeField] private TMP_Text pokemonLevelText;

    Coroutine spriteGetCorotuine;

    public Pokemon GetPokemon { get { return pokemon; } }

    /// <summary>
    /// Inicializa o Pok�mon no campo de batalha, atualizando o HUD com suas informa��es.
    /// </summary>
    /// <param name="pokemon">Inst�ncia do Pok�mon com seus dados prontos para batalha.</param>
    public virtual void SetupPlayer(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        pokemonActualLife = pokemon.GetPokemonActualLife;
        pokemonMaxLife = pokemon.GetPokemonMaxLife;
        updatePokemonLifeBar();

        pokemonNameText.text = pokemon.GetPokemonName.ToUpper();
        pokemonLevelText.text = $"Lv {pokemon.GetPokemonLevel}";
        int spritePosition = !enemyPokemon ? 0 : 1;
        pokemonSpriteImage.sprite = pokemon.GetPokemonSprites[spritePosition];
    }

    /// <summary>
    /// Atualiza a barra de vida visual e o texto com base na vida atual e m�xima do Pok�mon.
    /// </summary>
    private void updatePokemonLifeBar() {
        pokemonLifeSlider.fillAmount = (float)pokemonActualLife / pokemonMaxLife;
        if (pokemonLifeText != null) {
            pokemonLifeText.text = $"{pokemonActualLife}/{pokemonMaxLife}";
        }
    }
}
