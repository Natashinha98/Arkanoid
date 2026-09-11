using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("UI - Level Complete")]
    public GameObject levelCompletePanel;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI levelFinalScoreText;
    public TextMeshProUGUI nextLevelButtonText;

    private bool isLevelComplete = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if ((sceneName == "Scene1" || sceneName == "Scene2") && !isLevelComplete)
        {
            CheckLevelComplete();
        }
    }

    void CheckLevelComplete()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        if (bricks.Length == 0)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        isLevelComplete = true;

        // Pausa o jogo enquanto a tela intermediária aparece
        Time.timeScale = 0f;

        // Mostra o painel
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }

        // =====================================================
        // TÍTULO
        // =====================================================

        // Se o campo estiver configurado no Inspector, usa ele.
        // Caso contrário, procura automaticamente na cena.
        if (levelCompleteText == null)
        {
            GameObject titleObject = GameObject.Find("LevelCompleteText");

            if (titleObject != null)
            {
                levelCompleteText =
                    titleObject.GetComponent<TextMeshProUGUI>();
            }
        }

        if (levelCompleteText != null)
        {
            // Texto solicitado
            levelCompleteText.text = "He is getting furious !";

            // NÃO quebra a frase em duas linhas
            levelCompleteText.enableWordWrapping = false;

            // Permite que o texto ultrapasse a caixa sem quebrar
            levelCompleteText.overflowMode = TextOverflowModes.Overflow;

            // Tamanho da área do título
            RectTransform titleRect =
                levelCompleteText.GetComponent<RectTransform>();

            if (titleRect != null)
            {
                Vector2 size = titleRect.sizeDelta;
                size.x = 1000f;
                size.y = 150f;
                titleRect.sizeDelta = size;
            }

            // Mantém o texto em uma única linha
            levelCompleteText.alignment =
                TextAlignmentOptions.Center;

            // Tamanho adequado para caber em uma linha
            levelCompleteText.fontSize = 50f;
        }

        // =====================================================
        // ESCONDER PONTUAÇÃO
        // =====================================================

        if (levelFinalScoreText != null)
        {
            levelFinalScoreText.gameObject.SetActive(false);
        }

        // Mesmo que o campo não esteja configurado no Inspector,
        // procura automaticamente pelo objeto da pontuação.
        GameObject scoreObject =
            GameObject.Find("LevelFinalScoreText");

        if (scoreObject != null)
        {
            scoreObject.SetActive(false);
        }

        // =====================================================
        // BOTÃO
        // =====================================================

        // Primeiro tenta usar o campo configurado no Inspector.
        if (nextLevelButtonText != null)
        {
            nextLevelButtonText.text = "SMASH IT!";

            nextLevelButtonText.enableWordWrapping = false;
            nextLevelButtonText.overflowMode =
                TextOverflowModes.Overflow;

            nextLevelButtonText.alignment =
                TextAlignmentOptions.Center;
        }

        // Procura diretamente o botão da cena.
        // Isso corrige o problema de o texto do botão
        // continuar aparecendo como "Próximo Nível".
        GameObject nextButton =
            GameObject.Find("NextLevelButton");

        if (nextButton != null)
        {
            TextMeshProUGUI buttonText =
                nextButton.GetComponentInChildren<TextMeshProUGUI>(true);

            if (buttonText != null)
            {
                buttonText.text = "SMASH IT!";

                buttonText.enableWordWrapping = false;

                buttonText.overflowMode =
                    TextOverflowModes.Overflow;

                buttonText.alignment =
                    TextAlignmentOptions.Center;
            }
        }

        // =====================================================
        // PARAR A BOLA
        // =====================================================

        GameObject ball =
            GameObject.FindGameObjectWithTag("Ball");

        if (ball != null)
        {
            Rigidbody2D rb =
                ball.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    // =========================================================
    // BOTÃO SMASH IT!
    // =========================================================

    public void NextLevel()
    {
        // Volta o tempo ao normal
        Time.timeScale = 1f;

        isLevelComplete = false;

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        string currentScene =
            SceneManager.GetActiveScene().name;

        string nextScene = "";

        // Scene 1 -> Scene 2
        if (currentScene == "Scene1")
        {
            nextScene = "Scene2";
        }

        // Scene 2 -> Vitória
        else if (currentScene == "Scene2")
        {
            nextScene = "Vitoria";
        }

        if (!string.IsNullOrEmpty(nextScene))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.ResetLivesForNewLevel();
            }

            SceneManager.LoadScene(nextScene);
        }
    }
}