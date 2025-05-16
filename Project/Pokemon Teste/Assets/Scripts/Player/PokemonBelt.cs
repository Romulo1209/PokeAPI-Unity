using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa o cintur�o de Pok�mon de um treinador (jogador ou inimigo),
/// contendo at� 6 Pok�mons e controlando qual est� ativo no campo de batalha.
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
    /// Limpa todos os Pok�mons armazenados no cintur�o.
    /// </summary>
    public void ResetBelt()
    {
        pokemons.Clear();
    }

    /// <summary>
    /// Define o primeiro Pok�mon da lista como ativo por padr�o.
    /// </summary>
    public void SetupBelt() {
        activePokemon = pokemons[0];
    }

    /// <summary>
    /// Atualiza o campo de batalha com o Pok�mon atualmente ativo.
    /// </summary>
    public void SetupActivePokemon() {
        pokemonBattlegroundSide.SetupPlayer(ActivePokemon);
    }

    /// <summary>
    /// Cria uma inst�ncia completa de um Pok�mon a partir do modelo bruto e o adiciona ao cintur�o.
    /// Atribui 4 movimentos �nicos e as sprites (costas e frente).
    /// </summary>
    /// <param name="pokemon">Modelo de dados do Pok�mon retornado da API.</param>
    /// <param name="pokemonSprites">Lista de sprites do Pok�mon (back e front).</param>
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
