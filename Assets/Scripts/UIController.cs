using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text healthText;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthText(float currentHealth)
    {
        healthText.text = $"Health: {currentHealth}";
    }
}
