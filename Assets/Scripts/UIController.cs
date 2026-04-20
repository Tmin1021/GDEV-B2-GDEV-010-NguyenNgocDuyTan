using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject loseText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float winTime = 10f;

    private float _timer;
    private bool _lose;
    private bool _gameEnded;

    void OnEnable()
    {
        Player.OnHealthChange += UpdateHealthText;
        Player.OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        Player.OnHealthChange -= UpdateHealthText;
        Player.OnPlayerDied -= HandlePlayerDied;
    }

    void Start()
    {
        _timer = winTime;
        Time.timeScale = 1f;

        if (winText != null)
        {
            winText.SetActive(false);
        }

        if (loseText != null)
        {
            loseText.SetActive(false);
        }
    }

    void Update()
    {
        if (_gameEnded)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (timeText != null)
        {
            timeText.text = $"Timer: {Mathf.CeilToInt(Mathf.Max(_timer, 0f))}";
        }

        if (_timer <= 0f)
        {
            EndGame(true);
        }
    }

    private void UpdateHealthText(float currentHealth)
    {
        healthText.text = $"Health: {currentHealth}";
        if (currentHealth <= 0f)
        {
            _lose = true;
            EndGame(false);
        }
    }

    private void HandlePlayerDied()
    {
        _lose = true;
        EndGame(false);
    }

    private void EndGame(bool won)
    {
        if (_gameEnded)
        {
            return;
        }

        _gameEnded = true;
        Time.timeScale = 0f;

        if (winText != null)
        {
            winText.SetActive(won && !_lose);
        }

        if (loseText != null)
        {
            loseText.SetActive(!won || _lose);
        }
    }
}
