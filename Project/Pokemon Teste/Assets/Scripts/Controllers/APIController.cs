using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Respons�vel por interagir com a Pok�API, realizando requisi��es HTTP
/// para buscar dados de Pok�mon e seus movimentos.
/// </summary>
public class APIController : MonoBehaviour
{
    /// <summary>
    /// URL base da Pok�API utilizada para montar os endpoints das requisi��es.
    /// </summary>
    public string APIBaseURL = "https://pokeapi.co/api/v2";

    /// <summary>
    /// Valor m�ximo do range de pokemons que v�o ser gerados.
    /// </summary>
    public int MaxPokemonRange = 151;

    /// <summary>
    /// Busca um Pok�mon espec�fico pelo nome informado na Pok�API.
    /// </summary>
    /// <param name="name">Nome do Pok�mon a ser buscado (ex: "pikachu").</param>
    /// <param name="callback">Callback que retorna os dados do Pok�mon ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisi��o ass�ncrona.</returns>
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
    /// Busca um Pok�mon aleat�rio entre os IDs 1 e 150 da Pok�API.
    /// </summary>
    /// <param name="callback">Callback que retorna os dados do Pok�mon ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisi��o ass�ncrona.</returns>
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
    /// Busca m�ltiplos Pok�mons aleat�rios da Pok�API.
    /// </summary>
    /// <param name="amount">Quantidade de Pok�mons a buscar.</param>
    /// <param name="callback">Callback que retorna uma lista de modelos de dados dos Pok�mons.</param>
    /// <returns>Coroutine que realiza todas as requisi��es e retorna a lista final.</returns>
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
    /// Busca os dados completos de um movimento (move) de um Pok�mon usando a URL do movimento.
    /// </summary>
    /// <param name="moveUrl">URL completa do recurso de movimento na Pok�API.</param>
    /// <param name="callback">Callback com os dados do movimento ou null em caso de erro.</param>
    /// <returns>Coroutine que executa a requisi��o ass�ncrona.</returns>
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
