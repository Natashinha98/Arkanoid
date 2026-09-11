using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Inicia diretamente a primeira fase
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene1");
    }
    
    public void GoToRules()
    {
        // Vai para a tela de regras
        SceneManager.LoadScene("Regras");
    }
    
    public void GoToMenu()
    {
        // Volta para o menu inicial
        SceneManager.LoadScene("Inicial");
    }
    
    public void GoToPresentation()
    {
        // Mantido para compatibilidade com componentes antigos.
        // A apresentação foi removida do fluxo do jogo.
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene1");
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}