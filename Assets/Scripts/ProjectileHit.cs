using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.gameObject.name + " with tag: " + other.gameObject.tag);

        if (other.CompareTag("Enemy")) return;

        if (other.CompareTag("Player"))
        {
            PlayerHit Playerhealth = other.GetComponent<PlayerHit>();
            if (Playerhealth != null)
            {
                Playerhealth.damage(10); // 10 damage per hit
            }
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
