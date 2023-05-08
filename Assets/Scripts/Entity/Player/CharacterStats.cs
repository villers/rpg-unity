using System;
using UnityEngine;

public class CharacterStats : EntityStats
{
    public int baseHealth = 100;
    public int healthPerLevel = 10;
    public int healthPerVitality = 2;

    public int mana;
    public int maxMana;
    public int baseMana = 100;
    public int manaPerLevel = 10;
    public int manaPerIntelligence = 5;

    public int baseAttack = 5;
    public int attackPerLevel = 2;
    public int attackPerStrength = 1;

    public int baseDefense = 5;
    public int defensePerLevel = 2;
    public int defensePerVitality = 1;

    public float baseAttackSpeed = 1.0f;
    public int attackSpeedPerAgility = 50;

    public float baseCriticalChance = 1.0f;
    public int criticalChancePerLuck = 10;

    public float baseMovementSpeed = 3.0f;
    public int movementSpeedPerAgility = 50;

    public int baseDodge = 1;
    public int dodgePerAgility = 10;

    public int baseMagicResistance = 0;
    public int magicResistancePerIntelligence = 1;

    public int baseHit = 100;
    public int hitPerDexterity = 1;

    public float baseHpRegeneration = 0.1f;
    public int hpRegenerationPerVitality = 1;

    public float baseMpRegeneration = 0.1f;
    public int mpRegenerationPerIntelligence = 1;

    public int strength;
    public int agility;
    public int vitality;
    public int intelligence;
    public int dexterity;
    public int luck;

    public int skillPoints;
    public int experience;
    public int experienceToNextLevel;

    public float criticalChance;
    public int magicResistance;
    public float hpRegeneration;
    public float mpRegeneration;

    protected override void Start()
    {
        base.Start();
        experienceToNextLevel = GetExperienceToNextLevel(level);
        UpdateDerivedStats();

        health = maxHealth;
        mana = maxMana;
    }

    public void AddLevel(int points)
    {
        level += points;
    }

    public void AddSkillPoint(int points)
    {
        skillPoints += points;
    }

    public void GainExperience(int expGained)
    {
        experience += expGained;

        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            AddLevel(1);
            AddSkillPoint(1);
            experienceToNextLevel = GetExperienceToNextLevel(level);
            UpdateDerivedStats();
        }
    }

    private int GetExperienceToNextLevel(int currentLevel)
    {
        return 100 * currentLevel;
    }

    private void UpdateDerivedStats()
    {
        maxHealth = baseHealth + level * healthPerLevel + vitality * healthPerVitality;
        maxMana = baseMana + level * manaPerLevel + intelligence * manaPerIntelligence;

        attack = baseAttack + level * attackPerLevel + strength * attackPerStrength;
        defense = baseDefense + level * defensePerLevel + vitality * defensePerVitality;

        attackSpeed = baseAttackSpeed * (1 + (float)agility / attackSpeedPerAgility);
        criticalChance = baseCriticalChance + (float)luck / criticalChancePerLuck;
        movementSpeed = baseMovementSpeed + (float)agility / movementSpeedPerAgility;

        dodge = baseDodge + agility * dodgePerAgility;
        magicResistance = baseMagicResistance + intelligence * magicResistancePerIntelligence;
        hit = baseHit + dexterity * hitPerDexterity;

        hpRegeneration = baseHpRegeneration + (float)vitality / hpRegenerationPerVitality;
        mpRegeneration = baseMpRegeneration + (float)intelligence / mpRegenerationPerIntelligence;
        // Mettre à jour la vie et la mana actuelles en fonction des nouvelles valeurs maximales
        health = Mathf.Min(health, maxHealth);
        mana = Mathf.Min(mana, maxMana);
    }
}