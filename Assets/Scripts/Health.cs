using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public float redFlashDuration = 0.2f; 
    public float greenFlashDuration = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    public Image healthBarFill;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float percentage = currentHealth / (float)maxHealth;
        Debug.Log($" Before {currentHealth} {maxHealth} {percentage}");
        healthBarFill.rectTransform.localScale = new Vector3(percentage, 1, 1);
        Debug.Log($" After {healthBarFill.rectTransform.localScale}");
        //Debug.Log(healthBarFill.fillAmount);
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        gameObject.SetActive(false);       
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            FlashColor(Color.red, redFlashDuration);
        }
        else if (amount > 0)
        {
            FlashColor(Color.green, greenFlashDuration);
        }
    }

    private void FlashColor(Color color, float duration)
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashColorCoroutine(color, duration));
    }

    private IEnumerator FlashColorCoroutine(Color flashColor, float duration)
    {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = originalColor;

        flashCoroutine = null;
    }
}
