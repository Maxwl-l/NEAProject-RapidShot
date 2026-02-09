using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public int health = 10;
    public Color myColor = Color.red;
    public float flashDuration = 0.25f;

    public MeshRenderer mr;
    private Color originalColor;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        originalColor = mr.material.color;
    }

    public void damage(int dmg)
    {
        health = health - dmg;

        StartCoroutine(Flash());

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Flash()
    {
        mr.material.color = myColor;
        yield return new WaitForSeconds(flashDuration);
        mr.material.color = originalColor;
    }
}
