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
        textPos += new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        var textObject = Instantiate(damageText, textPos, Quaternion.identity);
        
        textObject.transform.parent = damageTextCanvas;
        
        textObject.ChangeText(damage.ToString());
    }
}
