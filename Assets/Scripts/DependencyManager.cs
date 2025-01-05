using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public static DependencyManager Instance { get; private set; } // Синглтон для доступу до менеджера

    [SerializeField] private Transform playerTransform; // Позиція гравця
    [SerializeField] private Health playerHealth; // Здоров'я гравця

    [SerializeField] private Transform arrowParent; // Контейнер для стріл

    void Awake()
    {
        // Ініціалізація синглтона
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject arrowPool = GameObject.FindGameObjectWithTag("ArrowPool");
            if (player != null)
            {
                playerTransform = player.transform;
                playerHealth = player.GetComponent<Health>();
            }
            if (arrowPool != null)
            {
                arrowParent = arrowPool.transform;
            }
        }
    }

    public void InitializeEnemy(EnemyController enemy)
    {
        if (playerTransform != null)
        {
            enemy.SetTarget(playerTransform);
        }

        if (enemy.TryGetComponent<Treant>(out Treant treant) && playerHealth != null)
        {
            treant.SetHeroHealth(playerHealth);
        }
    }

    public void InitializeBow(WeaponController bow)
    {
        bow.SetPlayer(playerTransform); // Встановлення Transform гравця в WeaponController
        BowController bowController = bow.GetComponent<BowController>();
        if (bowController != null)
        {
            bowController.SetArrowParent(playerTransform); // Встановлення ArrowParent
        }
    }

    public void InitializeSword(WeaponController sword)
    {
        sword.SetPlayer(playerTransform);
    }
}
