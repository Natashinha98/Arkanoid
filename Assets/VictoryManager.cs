using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI titleText;
    
    void Start()
    {
        // Verifica qual cena está carregada
        string sceneName = SceneManager.GetActiveScene().name;
        bool isVictory = sceneName == "Vitoria";
        
        // Atualiza o título
        if (titleText != null)
        {
            if (isVictory)
            {
                titleText.text = "THE LIGHT WINS";
                titleText.color = new Color(0.2f, 0.9f, 1f); // Azul/ciano espacial
            }
            else
            {
                titleText.text = "DARK SIDE WINS";
                titleText.color = new Color(0.55f, 0.65f, 1f); // Azul frio espacial
            }
        }
        
        // Mostra o score final
        if (scoreText != null && ScoreManager.Instance != null)
        {
            scoreText.text = $"Pontos: {ScoreManager.Instance.GetScore()}";
        }
        
        // Pausa o jogo
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        
        SceneManager.LoadScene("Scene1");
    }
    
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        
        SceneManager.LoadScene("Inicial");
    }
}