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

    // Initialisez les valeurs par d�faut dans Awake() si vous le souhaitez
    protected virtual void Awake()
    {
        // Vous pouvez d�finir ici des valeurs par d�faut pour les statistiques communes
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
        // Obtenir la chance d'esquiver en fonction des points de touch� et d'esquive
        float dodgeChance = CalculateDodgeChance(hit, target.dodge);

        // G�n�re un nombre al�atoire entre 0 et 1
        float randomValue = UnityEngine.Random.value;

        // V�rifie si l'attaque est esquiv�e
        if (randomValue <= dodgeChance) {
            // L'attaque a �t� esquiv�e, vous pouvez afficher un message ou un effet visuel ici
            target.ShowDamagePopup("MISS");
        } else
        {
            // L'attaque a r�ussi
            int damage = CalculateDamageDealt(target.defense);
            target.TakeDamage(damage);
        }
    }

    private int CalculateDamageDealt(int targetDefense)
    {
        // Vous pouvez utiliser une formule pour calculer les d�g�ts inflig�s en fonction des statistiques de l'entit�
        float attackModifier = Mathf.Max(0f, (attack - targetDefense) / 100f + 1f); //calcul du modificateur d'attaque
        float damage = Mathf.Clamp(attack * attackModifier, 0f, float.MaxValue); //calcul des d�g�ts inflig�s

        return Mathf.RoundToInt(damage);
    }

    // Autres m�thodes communes
    public void TakeDamage(int damage)
    {
        health -= damage; //retrait des points de vie en entiers
        ShowDamagePopup(damage); //affichage de la popup de d�g�ts inflig�s
        if (health <= 0)
        {
            OnEntityDeath?.Invoke(this);
            Die();
        }
    }
    protected virtual void Die()
    {
        // Faites quelque chose lorsque l'entit� meurt, par exemple d�truire le GameObject
        Destroy(gameObject);
    }

    // Ajoutez cette m�thode � la classe EntityStats
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
