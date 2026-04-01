using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 1; // A specifikáció szerinti sebzés mértéke
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mivel Trigger, itt 'Collider2D' az argumentum, nem 'Collision2D'
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(damageAmount);
            Debug.Log("Enemy triggerelt a playerrel, sebzés: " + damageAmount);
        }
    }
}