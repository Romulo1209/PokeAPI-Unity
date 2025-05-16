using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa um Pokémon instanciado no jogo, com dados calculados a partir do modelo bruto da PokéAPI.
/// Inclui nível, vida, sprites, movimentos e tipos.
/// </summary>
[Serializable]
public class Pokemon
{
    [SerializeField] private int pokemonID;
    [SerializeField] private string pokemonName;
    [SerializeField] private int pokemonLevel;
    [SerializeField] private int pokemonActualLife;
    [SerializeField] private int pokemonMaxLife;
    [SerializeField] private List<Sprite> pokemonSprites = new List<Sprite>();
    [SerializeField] private List<Move> pokemonMoves = new List<Move>();
    [SerializeField] private TypeSlot[] pokemonTypes;

    public int GetPokemonID { get { return pokemonID; } }
    public string GetPokemonName { get { return pokemonName; } }
    public int GetPokemonLevel { get { return pokemonLevel; } }
    public int GetPokemonActualLife { get { return pokemonActualLife; } }
    public int GetPokemonMaxLife { get { return pokemonMaxLife; } }
    public List<Sprite> GetPokemonSprites { get { return pokemonSprites; } }
    public List<Move> GetPokemonMoves { get { return pokemonMoves; } }
    public TypeSlot[] GetPokemonTypes { get { return pokemonTypes; } }

    /// <summary>
    /// Construtor da classe Pokémon que inicializa os dados com base no modelo da PokéAPI.
    /// </summary>
    /// <param name="model">Modelo bruto do Pokémon vindo da PokéAPI.</param>
    /// <param name="backSprite">Sprite da visão traseira do Pokémon.</param>
    /// <param name="frontSprite">Sprite da visão frontal do Pokémon.</param>
    /// <param name="moves">Lista de movimentos atribuídos a este Pokémon.</param>
    public Pokemon(PokemonDataModel model, Sprite backSprite, Sprite frontSprite, List<Move> moves)
    {
        this.pokemonID = model.GetPokemonId;
        this.pokemonName = model.GetPokemonName;
        this.pokemonLevel = UnityEngine.Random.Range(1, 100);
        this.pokemonMaxLife = PokemonUtils.CalculatePokemonHP(model.GetPokemonLife, pokemonLevel);
        this.pokemonActualLife = pokemonMaxLife;
        this.pokemonSprites.Add(backSprite);
        this.pokemonSprites.Add(frontSprite);
        this.pokemonMoves = moves;
        this.pokemonTypes = model.GetPokemonTypes;
    }
}
