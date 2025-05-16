using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia a lógica principal da batalha entre dois times de Pokémon.
/// É responsável por carregar dados da PokéAPI, instanciar sprites,
/// configurar as equipes e alternar entre os estados da batalha.
/// </summary>
public class BattleController : MonoBehaviour
{
    [SerializeField] private PokemonBelt playerPokemons;
    [SerializeField] private PokemonBelt enemyPokemons;

    [Header("Controllers")]
    [SerializeField] private APIController apiController;
    [SerializeField] private HUDController hudController;

    private bool battleGenerated = false;

    public PokemonBelt GetPlayerBelt { get { return playerPokemons; } }
    public Pokemon GetPlayerPokemonDataModel { get { return playerPokemons.GetPokemonBattlegroundSide.GetPokemon; } }
    public APIController GetAPIController { get { return apiController; } }
    public HUDController GetHUDController { get { return hudController; } }

    public static BattleController instance;

    /// <summary>
    /// Inicializa a instância singleton do controlador de batalha.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Inicia a batalha automaticamente ao carregar a cena.
    /// </summary>
    private void Start()
    {
        StartBattle();
    }

    /// <summary>
    /// Inicia o processo de geração de batalha.
    /// </summary>
    public void StartBattle()
    {
        StartCoroutine(GenerateBattleground());
    }

    /// <summary>
    /// Escuta comandos de teclado para reiniciar a batalha (Espaço)
    /// ou abrir o menu principal (ESC).
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (battleGenerated)
            {
                StartBattle();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetHUDController.OpenWindow(0);
        }
    }

    /// <summary>
    /// Gera as equipes de Pokémon do jogador e do inimigo, baixa suas sprites,
    /// posiciona no campo de batalha e ativa a HUD.
    /// </summary>
    /// <returns>Coroutine que aguarda o carregamento completo antes de ativar a batalha.</returns>
    IEnumerator GenerateBattleground()
    {
        battleGenerated = false;
        GetHUDController.OpenWindow(4);
        playerPokemons.ResetBelt();
        enemyPokemons.ResetBelt();

        List<PokemonDataModel> playerData = null;
        yield return StartCoroutine(GeneratePokemons(6, (data) => playerData = data));
        foreach (var pokemon in playerData)
        {
            List<Sprite> sprites = null;
            yield return StartCoroutine(getPokemonsSprites(pokemon, (result) => {
                sprites = result;
            }));
            playerPokemons.InsertPokemon(pokemon, sprites);
        }

        playerPokemons.SetupBelt();
        playerPokemons.GetPokemonBattlegroundSide.SetupPlayer(playerPokemons.ActivePokemon);

        List<PokemonDataModel> enemyData = null;
        yield return StartCoroutine(GeneratePokemons(6, (data) => enemyData = data));
        foreach (var pokemon in enemyData)
        {
            List<Sprite> sprites = null;
            yield return StartCoroutine(getPokemonsSprites(pokemon, (result) => {
                sprites = result;
            }));
            enemyPokemons.InsertPokemon(pokemon, sprites);
        }

        enemyPokemons.SetupBelt();
        enemyPokemons.GetPokemonBattlegroundSide.SetupPlayer(enemyPokemons.ActivePokemon);
        GetHUDController.OpenWindow(0);
        battleGenerated = true;
    }

    /// <summary>
    /// Faz o download das sprites front e back de um Pokémon e as retorna via callback.
    /// </summary>
    /// <param name="pokemon">Modelo de dados do Pokémon.</param>
    /// <param name="callback">Callback com a lista contendo 2 sprites (back e front).</param>
    /// <returns>Coroutine que baixa as sprites com tentativas em caso de erro.</returns>

    IEnumerator getPokemonsSprites(PokemonDataModel pokemon, Action<List<Sprite>> callback)
    {
        List<Sprite> pokemonSprites = new List<Sprite>();
        yield return StartCoroutine(PokemonUtils.LoadSpriteFromUrl(pokemon.GetPokemonSprite(false), (sprite) => {
            if (sprite != null){
                pokemonSprites.Add(sprite);
            }
            else {
                Debug.LogWarning("Sprite não carregada após múltiplas tentativas.");
            }
        }));
        yield return StartCoroutine(PokemonUtils.LoadSpriteFromUrl(pokemon.GetPokemonSprite(true), (sprite) => {
            if (sprite != null) {
                pokemonSprites.Add(sprite);
            }
            else {
                Debug.LogWarning("Sprite não carregada após múltiplas tentativas.");
            }
        }));
        callback?.Invoke(pokemonSprites);
    }

    /// <summary>
    /// Gera uma quantidade específica de Pokémon aleatórios via PokéAPI.
    /// </summary>
    /// <param name="pokemonQuantity">Número de pokémons a gerar.</param>
    /// <param name="callback">Callback com a lista de modelos retornados.</param>
    /// <returns>Coroutine que realiza a requisição e retorna os dados.</returns>

    IEnumerator GeneratePokemons(int pokemonQuantity, Action<List<PokemonDataModel>> callback)
    {
        List<PokemonDataModel> pokemons = null;

        yield return StartCoroutine(apiController.GetRandomPokemons(pokemonQuantity, (data) => pokemons = data));

        if (pokemons != null && pokemons.Count > 0)
        {
            callback?.Invoke(pokemons);
        }
    }
}