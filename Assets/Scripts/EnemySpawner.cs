using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab; // ������ ������
    [SerializeField] private int poolSize = 10; // ����� ����
    [SerializeField] private float spawnInterval = 2f; // �������� �� ��������
    [SerializeField] private Transform[] spawnPoints; // ����� ������ ������
    [SerializeField] private int maxEnemiesToSpawn = 20; // ����������� ������� ������, �� ������ ���� ���������

    private ObjectPool<EnemyController> enemyPool;
    private float spawnTimer;
    private int enemiesSpawned = 0; // ʳ������ ��� ����������� ������

    void Start()
    {
        // ���������� ��� ������
        enemyPool = new ObjectPool<EnemyController>(enemyPrefab, poolSize, transform);
    }

    void Update()
    {
        // ���� �� ������ ���������, ���������� �� ���������� ������ ������
        if (enemiesSpawned >= maxEnemiesToSpawn)
        {
            // �������� �� ��'���� � ����� "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool anyActive = false;

            // ����������, �� � ��� ���� �������� �����
            foreach (GameObject enemy in enemies)
            {
                if (enemy.activeSelf)
                {
                    anyActive = true;
                    break; // ���� ������� ��������� ������, ��������� ��������
                }
            }

            // ���� �������� ������ ����, ����������� �����
            if (!anyActive)
            {
                SceneManager.LoadSceneAsync("Level2");
                return;
            }
        }

        // �������� ������, ���� �� ��������� ����
        if (enemiesSpawned < maxEnemiesToSpawn)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = spawnInterval; // ������� ������
            }
        }
    }

    private void SpawnEnemy()
    {
        // �������� ������ � ����
        EnemyController enemy = enemyPool.Get();

        // ���������� ����� �� ���� � �����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        enemy.transform.position = spawnPoint.position;
        enemy.transform.rotation = spawnPoint.rotation;

        DependencyManager.Instance.InitializeEnemy(enemy);

        // �������� ������
        enemy.gameObject.SetActive(true);

        // �������� �������� ����������� ������
        enemiesSpawned++;
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.ReturnToPool(enemy);
    }
}