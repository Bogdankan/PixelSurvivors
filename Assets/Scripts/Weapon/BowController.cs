using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] private Transform arrowSpawnPoint; // ����� ������ ����
    [SerializeField] private Arrow arrowPrefab; // ������ �����
    [SerializeField] private int poolSize = 10; // ����� ���� ����
    [SerializeField] private float fireRate = 0.5f; // �������� �� ���������
    [SerializeField] private float arrowSpeed = 15f; // �������� �����
    [SerializeField] private Transform arrowParent; // ��'��� ��� ���������� ���� � ����

    private ObjectPool<Arrow> arrowPool;
    private float fireCooldown;

    void Start()
    {
        // ���������� ��� ����
        arrowPool = new ObjectPool<Arrow>(arrowPrefab, poolSize, arrowParent);
    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f) // ���� ������ ���������
        {
            ShootArrow(); // �������
            fireCooldown = fireRate; // ������� ������
        }
    }

    private void ShootArrow()
    {
        // �������� ����� � ����
        Arrow arrow = arrowPool.Get();

        // ������ ��������� ������� �� �������� �����
        arrow.transform.position = arrowSpawnPoint.position;
        arrow.transform.rotation = arrowSpawnPoint.rotation;

        // ³�'������ ����� �� Bow, ���� ���� ��� �������
        arrow.transform.SetParent(arrowParent);

        // ���������� �������� �������
        Vector2 direction = arrowSpawnPoint.up.normalized;

        // ��������� �����
        arrow.Launch(direction, arrowSpeed, () => arrowPool.ReturnToPool(arrow));
    }

    public void SetArrowParent(Transform parent)
    {
        arrowParent = parent;
    }
}
