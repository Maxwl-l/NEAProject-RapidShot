using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int health = 10;

    public void damage(int dmg)
    {
        health = health - dmg;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
