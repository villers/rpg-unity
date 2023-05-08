using System;
using TMPro;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    // Statistiques communes entre les personnages et les monstres
    public int level;
    public int health;
    public int maxHealth;
    public int attack;
    public int defense;
    public float attackRange = 2.0f;
    public float attackSpeed;
    public float movementSpeed;

    public int dodge = 10;
    public int hit = 90;


    public event Action<EntityStats> OnEntityDeath;

    // Initialisez les valeurs par défaut dans Awake() si vous le souhaitez
    protected virtual void Awake()
    {
        // Vous pouvez définir ici des valeurs par défaut pour les statistiques communes
    }

    protected virtual void Start()
    {
        
    }

    public float CalculateDodgeChance(int hitPoints, int dodgePoints)
    {
        float dodgeChance = (float)dodgePoints / (dodgePoints + hitPoints);
        return dodgeChance;
    }

    public void Attack(EntityStats target)
    {
        // Obtenir la chance d'esquiver en fonction des points de touché et d'esquive
        float dodgeChance = CalculateDodgeChance(hit, target.dodge);

        // Génère un nombre aléatoire entre 0 et 1
        float randomValue = UnityEngine.Random.value;

        // Vérifie si l'attaque est esquivée
        if (randomValue <= dodgeChance) {
            // L'attaque a été esquivée, vous pouvez afficher un message ou un effet visuel ici
            target.ShowDamagePopup("MISS");
        } else
        {
            // L'attaque a réussi
            int damage = CalculateDamageDealt(target.defense);
            target.TakeDamage(damage);
        }
    }

    private int CalculateDamageDealt(int targetDefense)
    {
        // Vous pouvez utiliser une formule pour calculer les dégâts infligés en fonction des statistiques de l'entité
        float attackModifier = Mathf.Max(0f, (attack - targetDefense) / 100f + 1f); //calcul du modificateur d'attaque
        float damage = Mathf.Clamp(attack * attackModifier, 0f, float.MaxValue); //calcul des dégâts infligés

        return Mathf.RoundToInt(damage);
    }

    // Autres méthodes communes
    public void TakeDamage(int damage)
    {
        health -= damage; //retrait des points de vie en entiers
        ShowDamagePopup(damage); //affichage de la popup de dégâts infligés
        if (health <= 0)
        {
            OnEntityDeath?.Invoke(this);
            Die();
        }
    }
    protected virtual void Die()
    {
        // Faites quelque chose lorsque l'entité meurt, par exemple détruire le GameObject
        Destroy(gameObject);
    }

    // Ajoutez cette méthode à la classe EntityStats
    public void ShowDamagePopup(int damage)
    {
        // Remplacez "DamagePopupPrefab" par le chemin de votre prefab dans le dossier "Assets"
        GameObject prefab = Resources.Load<GameObject>("Prefabs/DamagePopup");
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Vector3 newPosition = new Vector3(0, renderer.bounds.size.y + 0.2f) + transform.position;
        GameObject damagePopup = Instantiate(prefab, newPosition, Quaternion.identity);

        damagePopup.GetComponent<DamagePopup>().Setup(damage);
    }

    public void ShowDamagePopup(string damage)
    {
        // Remplacez "DamagePopupPrefab" par le chemin de votre prefab dans le dossier "Assets"
        GameObject prefab = Resources.Load<GameObject>("Prefabs/DamagePopup");
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Vector3 newPosition = new Vector3(0, renderer.bounds.size.y + 0.2f) + transform.position;
        GameObject damagePopup = Instantiate(prefab, newPosition, Quaternion.identity);

        damagePopup.GetComponent<DamagePopup>().Setup(damage);
    }
}
