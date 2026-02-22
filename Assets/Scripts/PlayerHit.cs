using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    [SerializeField] private PlayerHealthBar _healthBar;

    void Start()
    {
        
    }

    public void damage(int dmg)
    {
        health = health - dmg;
        _healthBar.UpdateHealthBar(maxHealth, health);
        Debug.Log("Player health: " + health);

        if (health <= 0)
        {
            Debug.Log("Player has died.");
            Destroy(this.gameObject);
        }
    }
}
