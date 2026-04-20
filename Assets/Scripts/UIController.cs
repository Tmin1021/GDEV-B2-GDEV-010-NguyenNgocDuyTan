using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;
    [SerializeField] float winTime = 10f;
    private float _timer;
    private bool _lose = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Player.OnHealthChange += UpdateHealthText;
    }
    void OnDisable()
    {
        Player.OnHealthChange -= UpdateHealthText;
    }

    void Start()
    {
        _timer = winTime;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer == 0f)
        {
            Time.timeScale = 0f;
            winText.SetActive(true);
        }
        if(_lose == true)
        {
            Time.timeScale = 0f;
            loseText.SetActive(true);
        }
    }

    void UpdateHealthText(float currentHealth)
    {
        healthText.text = $"Health: {currentHealth}";
    }
}
