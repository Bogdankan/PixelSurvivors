using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Treant : MonoBehaviour
{
    // Встановлюємо змінні для шкоди та часу затримки
    [SerializeField] private int damage = 10; // Встановлюємо тип int для шкоди
    [SerializeField] private float damageDelay = 2f;
    [SerializeField] Health _hero;
    // Перевірка, чи корутина вже запущена
    private Coroutine damageCoroutine;

    // Цей метод викликається, коли колайдер героя перетинається з тригером ворога
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Перевірка на наявність компонента "Health" на об'єкті, з яким перетинається тригер
        Health health = other.GetComponent<Health>();

        // Перевіряємо, чи це активний герой
        if (health == _hero && other.gameObject.activeInHierarchy && damageCoroutine == null)
        {
            // Запускаємо корутину для нанесення шкоди
            damageCoroutine = StartCoroutine(DealDamageWhileInZone(other));
        }
    }

    // Цей метод викликається, коли колайдер героя виходить з тригера ворога
    private void OnTriggerExit2D(Collider2D other)
    {
        // Перевірка на активність корутини
        if (damageCoroutine != null)
        {
            // Зупиняємо корутину
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // Корутин, який буде завдавати шкоду кожні кілька секунд, поки герой перебуває в зоні
    private IEnumerator DealDamageWhileInZone(Collider2D other)
    {
        // Поки герой знаходиться в зоні, завдаємо шкоди з певною затримкою
        while (other != null && other.gameObject.activeInHierarchy)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                // Завдаємо шкоду герою
                health.TakeDamage(damage);
            }

            // Чекаємо зазначену кількість секунд перед наступним ударом
            yield return new WaitForSeconds(damageDelay);
        }

        // Корутина більше не активна
        damageCoroutine = null;
    }

    public void SetHeroHealth(Health hero)
    {
        _hero = hero;
    }
}
