using System;

/// <summary>
/// Representa os dados detalhados de um movimento (move) de um Pokémon,
/// obtidos a partir da PokéAPI. Inclui nome, tipo e PP (pontos de uso).
/// </summary>
[Serializable]
public class MoveDataModel
{
    public string name;
    public MoveType type;
    public int pp;

    public string GetMoveName { get { return name; } }
    public int GetMovePP { get { return pp; } }
    public string GetMoveType { get { return type.name; } }
}

[Serializable]
public class MoveType
{
    public string name;
    public string url;
}