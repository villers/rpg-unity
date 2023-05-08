using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    // Statistiques spécifiques aux monstres
    public int experienceReward = 10;

    protected override void Awake()
    {
        base.Awake();
        // Vous pouvez définir ici des valeurs par défaut pour les statistiques spécifiques aux monstres
    }

    // Autres méthodes spécifiques aux monstres
    private void Start()
    {
        OnEntityDeath += EnemyStats_OnEntityDeath;
    }

    private void EnemyStats_OnEntityDeath(EntityStats entityStats)
    {
        var playerStats = FindObjectOfType<PlayerMovement>().GetComponent<CharacterStats>();
        playerStats.GainExperience(experienceReward);
    }

    private void OnDestroy()
    {
        OnEntityDeath -= EnemyStats_OnEntityDeath;
    }
}