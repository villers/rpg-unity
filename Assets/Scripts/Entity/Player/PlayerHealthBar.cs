using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider playerHealthSlider;
    private CharacterStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerMovement>().GetComponent<CharacterStats>();
    }

    private void Update()
    {
        float healthPercentage = (float)playerStats.health / playerStats.maxHealth;
        playerHealthSlider.value = healthPercentage;
    }
}