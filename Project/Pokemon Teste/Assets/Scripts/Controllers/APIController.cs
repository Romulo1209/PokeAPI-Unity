using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Responsável por interagir com a PokéAPI, realizando requisições HTTP
/// para buscar dados de Pokémon e seus movimentos.
/// </summary>
public class APIController : MonoBehaviour
{
    /// <summary>
    /// URL base da PokéAPI utilizada para montar os endpoints das requisições.
    /// </summary>
    public string APIBaseURL = "https://pokeapi.co/api/v2";

    /// <summary>
    /// Valor máximo do range de pokemons que vão ser gerados.
    /// </summary>
    public int MaxPokemonRange = 151;

    /// <summary>
    /// Busca um Pokémon específico pelo nome informado na PokéAPI.
    /// </summary>
    /// <param name="name">Nome do Pokémon a ser buscado (ex: "pikachu").</param>
    /// <param name="callback">Callback que retorna os dados do Pokémon ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisição assíncrona.</returns>
    public IEnumerator GetPokemonByName(string name, Action<PokemonDataModel> callback)
    {
        string url = $"{APIBaseURL}/pokemon/{name}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error fetching Pokemon {name}: {request.error}");
                callback?.Invoke(null);
            }
            else
            {
                PokemonDataModel pokemon = JsonUtility.FromJson<PokemonDataModel>(request.downloadHandler.text);
                callback?.Invoke(pokemon);
            }
        }
    }

    /// <summary>
    /// Busca um Pokémon aleatório entre os IDs 1 e 150 da PokéAPI.
    /// </summary>
    /// <param name="callback">Callback que retorna os dados do Pokémon ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisição assíncrona.</returns>
    public IEnumerator GetRandomPokemon(Action<PokemonDataModel> callback) {
        int randomId = UnityEngine.Random.Range(1, MaxPokemonRange);
        string url = $"https://pokeapi.co/api/v2/pokemon/{randomId}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {
                Debug.LogError($"Error fetching Pokemon ID {randomId}: {request.error}");
                callback?.Invoke(null);
            }
            else {
                PokemonDataModel pokemon = JsonUtility.FromJson<PokemonDataModel>(request.downloadHandler.text);
                callback?.Invoke(pokemon);
            }
        }
    }

    /// <summary>
    /// Busca múltiplos Pokémons aleatórios da PokéAPI.
    /// </summary>
    /// <param name="amount">Quantidade de Pokémons a buscar.</param>
    /// <param name="callback">Callback que retorna uma lista de modelos de dados dos Pokémons.</param>
    /// <returns>Coroutine que realiza todas as requisições e retorna a lista final.</returns>
    public IEnumerator GetRandomPokemons(int amount, Action<List<PokemonDataModel>> callback)
    {
        List<PokemonDataModel> pokemons = new List<PokemonDataModel>();

        for (int i = 0; i < amount; i++)
        {
            bool done = false;
            PokemonDataModel pokemon = null;

            yield return StartCoroutine(GetRandomPokemon((data) => {
                pokemon = data;
                done = true;
            }));

            while (!done)
                yield return null;

            if (pokemon != null)
                pokemons.Add(pokemon);
        }

        callback?.Invoke(pokemons);
    }

    /// <summary>
    /// Busca os dados completos de um movimento (move) de um Pokémon usando a URL do movimento.
    /// </summary>
    /// <param name="moveUrl">URL completa do recurso de movimento na PokéAPI.</param>
    /// <param name="callback">Callback com os dados do movimento ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisição assíncrona.</returns>
    public IEnumerator GetMoveData(string moveUrl, Action<MoveDataModel> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(moveUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Erro ao buscar Move: {request.error}");
                callback?.Invoke(null);
            }
            else
            {
                MoveDataModel moveData = JsonUtility.FromJson<MoveDataModel>(request.downloadHandler.text);
                callback?.Invoke(moveData);
            }
        }
    }
}
