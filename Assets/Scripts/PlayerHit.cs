using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    public int health = 100;

    void Start()
    {
    }

    public void damage(int dmg)
    {
        health = health - dmg;
        Debug.Log("Player health: " + health);

        if (health <= 0)
        {
            Debug.Log("Player has died.");
            Destroy(this.gameObject);
        }
    }
}
