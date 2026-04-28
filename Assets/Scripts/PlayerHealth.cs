using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Életerő Beállítások")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("UI Referenciák")]
    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Sebezhetetlenség")]
    public float iFrameDuration = 1.5f;
    private bool isInvincible = false;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        // Ha épp sebezhetetlen, nem csinálunk semmit
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log("Sebzés történt! Aktuális élet: " + currentHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityTimer());
        }
    }

    private IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        yield return new WaitForSeconds(iFrameDuration);
        isInvincible = false;
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
        }
    }

    public void Die()
    {
        Debug.Log("Meghaltál!");
        Respawn();
    }

    private void Respawn()
    {
        transform.position = startPosition;
        Input.ResetInputAxes();
        if (TryGetComponent(out Rigidbody2D rb))
            rb.linearVelocity = Vector2.zero;

        currentHealth = maxHealth;

        StopAllCoroutines();
        StartCoroutine(InvincibilityTimer());

        UpdateHealthUI();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isInvincible = false;
        UpdateHealthUI();
    }
}