using UnityEngine;

public class DependencyManager : MonoBehaviour
{
    public static DependencyManager Instance { get; private set; } // �������� ��� ������� �� ���������

    [SerializeField] private Transform playerTransform; // ������� ������
    [SerializeField] private Health playerHealth; // ������'� ������

    [SerializeField] private Transform arrowParent; // ��������� ��� ����

    void Awake()
    {
        // ����������� ���������
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
        bow.SetPlayer(playerTransform); // ������������ Transform ������ � WeaponController
        BowController bowController = bow.GetComponent<BowController>();
        if (bowController != null)
        {
            bowController.SetArrowParent(playerTransform); // ������������ ArrowParent
        }
    }

    public void InitializeSword(WeaponController sword)
    {
        sword.SetPlayer(playerTransform);
    }
}
