using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    [SerializeField] private HealthBar _healthBar;

    public Color myColor = Color.red;
    public float flashDuration = 0.25f;

    public MeshRenderer mr;
    private Color originalColor;

    WaveSpawn Spawner;



    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        originalColor = mr.material.color;

    }

    public void SetSpawner(WaveSpawn _spawner)
    {
        Spawner = _spawner;
    }

    public void damage(int dmg)
    {
        health = health - dmg;

        _healthBar.UpdateHealthBar(maxHealth, health);

        StartCoroutine(Flash());

        if (health <= 0)
        {

            if (Spawner !=null)
                Spawner.currentMonster.Remove(this.gameObject);

            Destroy(this.gameObject); // waits for sound to finish
        }
    }
    IEnumerator Flash()
    {
        mr.material.color = myColor;
        yield return new WaitForSeconds(flashDuration);
        mr.material.color = originalColor;
    }
}
