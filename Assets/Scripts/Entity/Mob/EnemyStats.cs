using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    // Statistiques sp�cifiques aux monstres
    public int experienceReward = 10;

    protected override void Awake()
    {
        base.Awake();
        // Vous pouvez d�finir ici des valeurs par d�faut pour les statistiques sp�cifiques aux monstres
    }

    // Autres m�thodes sp�cifiques aux monstres
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