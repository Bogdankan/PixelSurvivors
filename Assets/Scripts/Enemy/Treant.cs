using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Treant : MonoBehaviour
{
    // ������������ ���� ��� ����� �� ���� ��������
    [SerializeField] private int damage = 10; // ������������ ��� int ��� �����
    [SerializeField] private float damageDelay = 2f;
    [SerializeField] Health _hero;
    // ��������, �� �������� ��� ��������
    private Coroutine damageCoroutine;

    // ��� ����� �����������, ���� �������� ����� ������������ � �������� ������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �������� �� �������� ���������� "Health" �� ��'���, � ���� ������������ ������
        Health health = other.GetComponent<Health>();

        // ����������, �� �� �������� �����
        if (health == _hero && other.gameObject.activeInHierarchy && damageCoroutine == null)
        {
            // ��������� �������� ��� ��������� �����
            damageCoroutine = StartCoroutine(DealDamageWhileInZone(other));
        }
    }

    // ��� ����� �����������, ���� �������� ����� �������� � ������� ������
    private void OnTriggerExit2D(Collider2D other)
    {
        // �������� �� ��������� ��������
        if (damageCoroutine != null)
        {
            // ��������� ��������
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // �������, ���� ���� ��������� ����� ���� ����� ������, ���� ����� �������� � ���
    private IEnumerator DealDamageWhileInZone(Collider2D other)
    {
        // ���� ����� ����������� � ���, ������� ����� � ������ ���������
        while (other != null && other.gameObject.activeInHierarchy)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                // ������� ����� �����
                health.TakeDamage(damage);
            }

            // ������ ��������� ������� ������ ����� ��������� ������
            yield return new WaitForSeconds(damageDelay);
        }

        // �������� ����� �� �������
        damageCoroutine = null;
    }

    public void SetHeroHealth(Health hero)
    {
        _hero = hero;
    }
}
