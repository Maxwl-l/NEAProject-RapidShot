using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthbarSprite;
    public void UpdateHealthBar(float maxhealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxhealth;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
