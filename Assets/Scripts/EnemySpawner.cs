using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab; // Префаб ворога
    [SerializeField] private int poolSize = 10; // Розмір пулу
    [SerializeField] private float spawnInterval = 2f; // Інтервал між спавнами
    [SerializeField] private Transform[] spawnPoints; // Точки спавну ворогів
    [SerializeField] private int maxEnemiesToSpawn = 20; // Максимальна кількість ворогів, які можуть бути заспавнені

    private ObjectPool<EnemyController> enemyPool;
    private float spawnTimer;
    private int enemiesSpawned = 0; // Кількість уже заспавнених ворогів

    void Start()
    {
        // Ініціалізуємо пул ворогів
        enemyPool = new ObjectPool<EnemyController>(enemyPrefab, poolSize, transform);
    }

    void Update()
    {
        // Якщо всі вороги заспавнені, перевіряємо чи залишились активні вороги
        if (enemiesSpawned >= maxEnemiesToSpawn)
        {
            // Отримуємо всі об'єкти з тегом "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool anyActive = false;

            // Перевіряємо, чи є хоч один активний ворог
            foreach (GameObject enemy in enemies)
            {
                if (enemy.activeSelf)
                {
                    anyActive = true;
                    break; // Якщо знайшли активного ворога, зупиняємо перевірку
                }
            }

            // Якщо активних ворогів немає, переключаємо сцену
            if (!anyActive)
            {
                SceneManager.LoadSceneAsync("Level2");
                return;
            }
        }

        // Спавнимо ворогів, якщо не досягнуто ліміту
        if (enemiesSpawned < maxEnemiesToSpawn)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval; // Скидаємо таймер
            }
        }
    }

    private void SpawnEnemy()
    {
        // Отримуємо ворога з пулу
        EnemyController enemy = enemyPool.Get();

        // Випадковий спавн на одній з точок
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        DependencyManager.Instance.InitializeEnemy(enemy);

        // Активуємо ворога
        enemy.gameObject.SetActive(true);

        // Збільшуємо лічильник заспавнених ворогів
        enemiesSpawned++;
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.ReturnToPool(enemy);
    }
}