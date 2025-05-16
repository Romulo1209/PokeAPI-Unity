using UnityEngine;

/// <summary>
/// Controlador respons�vel por gerenciar a interface de usu�rio (HUD) da batalha.
/// Abre e fecha janelas com base em um �ndice fornecido, e pode passar dados de Pok�mon.
/// </summary>
public class HUDController : MonoBehaviour
{
    /// <summary>
    /// Lista de janelas dispon�veis no HUD. A posi��o no array define o ID usado para abri-las.
    /// </summary>
    [Header("Window Controller Parameters")]
    [SerializeField] private WindowBase[] windows;

    /// <summary>
    /// Abre uma janela espec�fica com base no ID fornecido, e fecha todas as outras.
    /// </summary>
    /// <param name="id">�ndice da janela no array de janelas.</param>
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
    /// Abre uma janela espec�fica com base no ID fornecido e envia os dados do Pok�mon selecionado.
    /// Fecha todas as outras janelas.
    /// </summary>
    /// <param name="id">�ndice da janela no array de janelas.</param>
    /// <param name="pokemon">Pok�mon que ser� usado pela janela, se aplic�vel.</param>
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
