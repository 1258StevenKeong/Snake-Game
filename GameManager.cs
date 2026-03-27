using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject StartPanel;
    public TMP_Text ScoreText;

    public bool IsPlaying { get; private set; }

    private int _score;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        IsPlaying = false;
    }

    public void StartGame()
    {
        // 檢查是否已在 Inspector 指派 StartPanel，避免 UnassignedReferenceException
        if (StartPanel == null)
        {
            Debug.LogError("[GameManager] StartPanel Not set (Please assign Start Panel to GameManager.StartPanel in the Inspector).");
            return;
        }

        _score = 0;
        UpdateScoreUI();
        StartPanel.SetActive(false);
        IsPlaying = true;

        Snake snake = FindObjectOfType<Snake>();
        if (snake != null) snake.ResetState();
        else Debug.LogWarning("[GameManager] Snake cannot be found. Please check if there is a Snake object in the scene.");
    }

    public void EndGame()
    {
        IsPlaying = false;

        if (StartPanel == null)
        {
            Debug.LogWarning("[GameManager] EndGame: StartPanel Not configured; the Start panel cannot be displayed.");
            return;
        }

        StartPanel.SetActive(true);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (ScoreText != null) ScoreText.text = $"Score: {_score}";
        else Debug.LogWarning("[GameManager] ScoreText Since it hasn't been assigned in the Inspector, the score display cannot be updated.");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("[GameManager] Editor: Stop PlayMode (QuitGame invoked)");
        EditorApplication.isPlaying = false;
#else
        Debug.Log("[GameManager] Application.Quit invoked");
        Application.Quit();
#endif
    }
}