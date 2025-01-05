using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Action onReturnToPool;
    private Damaged damaged;
    void Awake()
    {
        damaged = GetComponent<Damaged>();
        if (damaged == null)
        {
            Debug.LogError("Damaged компонент не знайдено на Bow!");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float speed, Action returnToPoolCallback)
    {
        onReturnToPool = returnToPoolCallback;

        // Задаємо початкову швидкість стрілі
        rb.linearVelocity = direction * speed;

        // Автоматичне повернення до пулу через час життя
        Invoke(nameof(ReturnToPool), 3f); // Lifetime = 3 секунди
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Логіка завдання шкоди
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            damaged.ApplyDamage(other.gameObject);
        }

        ReturnToPool();
    }

    private void ReturnToPool()
    {
        rb.linearVelocity = Vector2.zero; // Зупиняємо рух
        rb.angularVelocity = 0f;   // Зупиняємо обертання
        gameObject.SetActive(false);

        CancelInvoke(); // Зупиняємо таймер повернення
        onReturnToPool?.Invoke();
    }
}
