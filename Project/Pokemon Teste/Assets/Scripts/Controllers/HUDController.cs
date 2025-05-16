using UnityEngine;

/// <summary>
/// Controlador responsável por gerenciar a interface de usuário (HUD) da batalha.
/// Abre e fecha janelas com base em um índice fornecido, e pode passar dados de Pokémon.
/// </summary>
public class HUDController : MonoBehaviour
{
    /// <summary>
    /// Lista de janelas disponíveis no HUD. A posição no array define o ID usado para abri-las.
    /// </summary>
    [Header("Window Controller Parameters")]
    [SerializeField] private WindowBase[] windows;

    /// <summary>
    /// Abre uma janela específica com base no ID fornecido, e fecha todas as outras.
    /// </summary>
    /// <param name="id">Índice da janela no array de janelas.</param>
    public void OpenWindow(int id) {
        if(id > windows.Length) {
            Debug.LogError("Window ID doesn't match with the Windows lenght.");
            return;
        }

        foreach(WindowBase window in windows) {
            window.CloseWindow();
        }
        windows[id].OpenWindow();
    }

    /// <summary>
    /// Abre uma janela específica com base no ID fornecido e envia os dados do Pokémon selecionado.
    /// Fecha todas as outras janelas.
    /// </summary>
    /// <param name="id">Índice da janela no array de janelas.</param>
    /// <param name="pokemon">Pokémon que será usado pela janela, se aplicável.</param>
    public void OpenWindow(int id, Pokemon pokemon)
    {
        if (id > windows.Length)
        {
            Debug.LogError("Window ID doesn't match with the Windows lenght.");
            return;
        }

        foreach (WindowBase window in windows)
        {
            window.CloseWindow();
        }
        windows[id].OpenWindow(pokemon);
    }
}
