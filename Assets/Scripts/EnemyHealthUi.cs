using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUi : MonoBehaviour
{
    [SerializeField] private RectTransform damageTextCanvas;
    [SerializeField] private WorldSpaceText damageText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Enemy enemy;

    private void Start()
    {
        enemy.OnDamage += OnDamage;
        healthBar.maxValue = enemy.StartHealth;
        healthBar.value = enemy.StartHealth;
    }

    private void OnDamage(WeaponHit hit, float health)
    {
        ShowTextDamage(hit.damage);
        UpdateHealthBar(health);
    }

    private void UpdateHealthBar(float health)
    {
        healthBar.value = health;
    }
    
    private void ShowTextDamage(float damage)
    {
        var textPos = transform.position;
        textPos += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(1.0f, 1.5f), Random.Range(-1.0f, 1.0f));
        WorldSpaceText textObject = WorldSpaceText.Instantiate(damageText, textPos, .2f, 10);
        
        textObject.transform.parent = damageTextCanvas;
        
        textObject.ChangeText(damage.ToString());
    }
}
