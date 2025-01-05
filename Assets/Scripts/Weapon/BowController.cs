using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] private Transform arrowSpawnPoint; // Точка спавну стріл
    [SerializeField] private Arrow arrowPrefab; // Префаб стріли
    [SerializeField] private int poolSize = 10; // Розмір пулу стріл
    [SerializeField] private float fireRate = 0.5f; // Затримка між пострілами
    [SerializeField] private float arrowSpeed = 15f; // Швидкість стріли
    [SerializeField] private Transform arrowParent; // Об'єкт для організації стріл у сцені

    private ObjectPool<Arrow> arrowPool;
    private float fireCooldown;

    void Start()
    {
        // Ініціалізуємо пул стріл
        arrowPool = new ObjectPool<Arrow>(arrowPrefab, poolSize, arrowParent);
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f) // Якщо таймер закінчився
        {
            ShootArrow(); // Стрільба
            fireCooldown = fireRate; // Скидаємо таймер
        }
    }

    private void ShootArrow()
    {
        // Отримуємо стрілу з пулу
        Arrow arrow = arrowPool.Get();

        // Задаємо початкову позицію та орієнтацію стріли
        arrow.transform.position = arrowSpawnPoint.position;
        arrow.transform.rotation = arrowSpawnPoint.rotation;

        // Від'єднуємо стрілу від Bow, якщо вона досі дочірня
        arrow.transform.SetParent(arrowParent);

        // Обчислюємо напрямок стрільби
        Vector2 direction = arrowSpawnPoint.up.normalized;

        // Запускаємо стрілу
        arrow.Launch(direction, arrowSpeed, () => arrowPool.ReturnToPool(arrow));
    }

    public void SetArrowParent(Transform parent)
    {
        arrowParent = parent;
    }
}
