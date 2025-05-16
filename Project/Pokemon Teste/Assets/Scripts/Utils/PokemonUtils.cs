using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Cont�m utilit�rios est�ticos relacionados a Pok�mon, como carregamento de sprites
/// e c�lculo de atributos (HP).
/// </summary>
public static class PokemonUtils
{
    /// <summary>
    /// Realiza o download de uma imagem de sprite de um Pok�mon a partir de uma URL e a converte para Sprite.
    /// Tenta m�ltiplas vezes se houver erro 429 (Too Many Requests).
    /// </summary>
    /// <param name="url">URL da imagem a ser baixada.</param>
    /// <param name="callback">Callback com o Sprite criado ou null em caso de falha.</param>
    /// <param name="maxRetries">N�mero m�ximo de tentativas em caso de erro 429.</param>
    /// <returns>Coroutine que lida com o download da sprite.</returns>
    public static IEnumerator LoadSpriteFromUrl(string url, Action<Sprite> callback, int maxRetries = 20)
    {
        int attempt = 0;
        float delay = 1f;

        while (attempt < maxRetries)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);

                    texture.filterMode = FilterMode.Point;
                    texture.Apply(true, false);

                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    callback?.Invoke(sprite);
                    yield break; 
                }
                else if ((int)request.responseCode == 429)
                {
                    Debug.LogWarning($"[SpriteLoader] Too many requests. Retrying in {delay} seconds...");
                    yield return new WaitForSeconds(delay);
                    attempt++;
                    delay *= 2f; 
                }
                else
                {
                    Debug.LogError($"[SpriteLoader] Failed to download sprite: {request.error}");
                    callback?.Invoke(null);
                    yield break;
                }
            }
        }

        Debug.LogError($"[SpriteLoader] Max retry attempts reached for URL: {url}");
        callback?.Invoke(null);
    }

    /// <summary>
    /// Calcula os pontos de vida (HP) de um Pok�mon com base na f�rmula oficial da franquia.
    /// Considera IV e EV como 0, e natureza neutra.
    /// </summary>
    /// <param name="baseHP">Valor base do HP do Pok�mon.</param>
    /// <param name="level">N�vel atual do Pok�mon.</param>
    /// <returns>Valor total de HP calculado.</returns>
    public static int CalculatePokemonHP(int baseHP, int level)
    {
        int hp = (((2 * baseHP) * level) / 100) + level + 10;
        return hp;
    }
}
