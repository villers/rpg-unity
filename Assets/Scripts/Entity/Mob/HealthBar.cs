using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    private EntityStats entityStats;

    private void Start()
    {
        entityStats = GetComponentInParent<EntityStats>();
    }

    private void Update()
    {
        float healthPercentage = (float)entityStats.health / entityStats.maxHealth;
        healthBarSlider.value = healthPercentage;
    }
}
