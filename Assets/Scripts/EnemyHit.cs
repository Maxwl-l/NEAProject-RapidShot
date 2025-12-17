using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int health = 10;
    public Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;
    }

    public void damage(int dmg)
    {
        health = health - dmg;
        
        if (health < 5  && health > 0)
        {
            renderer.material.color = Color.yellow;
        }
        if (health < 3 && health > 0)
        {
            renderer.material.color = Color.red;
        }
        if (health <= 0)
        {
            
            Destroy(this.gameObject);
        }
    }
}
