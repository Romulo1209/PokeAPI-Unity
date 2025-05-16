using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa o cinturão de Pokémon de um treinador (jogador ou inimigo),
/// contendo até 6 Pokémons e controlando qual está ativo no campo de batalha.
/// </summary>
[Serializable]
public class PokemonBelt
{
    [SerializeField] private PokemonBase pokemonBattlegroundSide;
    [SerializeField] private Pokemon activePokemon;
    [SerializeField] private List<Pokemon> pokemons = new List<Pokemon>();

    public PokemonBase GetPokemonBattlegroundSide { get { return pokemonBattlegroundSide; } }
    public Pokemon ActivePokemon { get { return activePokemon; } set { activePokemon = value; SetupActivePokemon(); } }
    public List<Pokemon> GetPokemons { get { return pokemons; } }

    /// <summary>
    /// Limpa todos os Pokémons armazenados no cinturão.
    /// </summary>
    public void ResetBelt()
    {
        pokemons.Clear();
    }

    /// <summary>
    /// Define o primeiro Pokémon da lista como ativo por padrão.
    /// </summary>
    public void SetupBelt() {
        activePokemon = pokemons[0];
    }

    /// <summary>
    /// Atualiza o campo de batalha com o Pokémon atualmente ativo.
    /// </summary>
    public void SetupActivePokemon() {
        pokemonBattlegroundSide.SetupPlayer(ActivePokemon);
    }

    /// <summary>
    /// Cria uma instância completa de um Pokémon a partir do modelo bruto e o adiciona ao cinturão.
    /// Atribui 4 movimentos únicos e as sprites (costas e frente).
    /// </summary>
    /// <param name="pokemon">Modelo de dados do Pokémon retornado da API.</param>
    /// <param name="pokemonSprites">Lista de sprites do Pokémon (back e front).</param>
    public void InsertPokemon(PokemonDataModel pokemon, List<Sprite> pokemonSprites) {
        if(pokemons.Count >= 6) {
            Debug.LogError("Can't add more pokemons to belt.");
            return;
        }

        List<Move> moves = new List<Move>();
        for (int i = 0; i < 4; i++) {
            moves.Add(pokemon.GetRandomUniqueMove(moves));
        }

        Pokemon _pokemon = new Pokemon(pokemon, pokemonSprites[0], pokemonSprites[1], moves);
        pokemons.Add(_pokemon);
    }
}
