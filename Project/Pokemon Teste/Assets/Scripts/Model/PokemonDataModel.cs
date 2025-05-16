using System;
using System.Collections.Generic;

/// <summary>
/// Representa o modelo bruto retornado pela PokéAPI para um Pokémon,
/// contendo seus atributos, stats, movimentos, tipos e sprites.
/// </summary>
[Serializable]
public class PokemonDataModel
{
    public int id;
    public string name;
    public int level;
    public Stat[] stats;
    public MoveSlot[] moves;
    public TypeSlot[] types;
    public Sprites sprites;

    public int GetPokemonId { get { return id; } }
    public string GetPokemonName { get { return name; } }
    public int GetPokemonLife { get { return stats[0].base_stat; } }
    public TypeSlot[] GetPokemonTypes { get { return types; } }
    public Move GetRandomUniqueMove(List<Move> movesAlreadySelected, int maxAttempts = 3)
    {
        if (moves == null || moves.Length == 0)
            return null;

        Move selectedMove = null;
        int attempts = 0;

        while (attempts < maxAttempts) {
            var randomMove = moves[UnityEngine.Random.Range(0, moves.Length)].move;

            if (randomMove != null && !movesAlreadySelected.Contains(randomMove)) {
                selectedMove = randomMove;
                break;
            }

            attempts++;
        }

        return selectedMove;
    }
    public string GetPokemonSprite(bool enemy) { 
        if(!enemy) {
            return sprites.back_default;
        } else {
            return sprites.front_default;
        }
    }
}

[Serializable]
public class Stat
{
    public int base_stat;
    public StatDetail stat;
}

[Serializable]
public class StatDetail
{
    public string name;
}

[Serializable]
public class MoveSlot
{
    public Move move;
}

[Serializable]
public class Move
{
    public string name;
    public string url;

    public string GetMoveName { get { return name; } }
}

[Serializable]
public class Sprites
{
    public string back_default;
    public string back_female;
    public string back_shiny;
    public string back_shiny_female;
    public string front_default;
    public string front_female;
    public string front_shiny;
    public string front_shiny_female;
}

[Serializable]
public class TypeSlot
{
    public int slot;
    public TypeInfo type;
}

[Serializable]
public class TypeInfo
{
    public string name;
    public string url;
}