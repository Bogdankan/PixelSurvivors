using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        // ����������, �� ��� ���� ������� AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �������� ��'��� �� �������
        }
        else
        {
            Destroy(gameObject); // ��������� ��������
        }
    }
}
