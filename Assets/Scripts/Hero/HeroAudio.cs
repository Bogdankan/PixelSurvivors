using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // ������� �����
    [SerializeField] private AudioClip walkSound; // ���� ������
    [SerializeField] private AudioClip swordSwingSound; // ���� ����� �����
    [SerializeField] private float walkSoundInterval = 0.5f; // �������� �� ������� ������

    private HeroController playerController; // ������, �� ���� �������
    private float walkTimer; // ������ ��� ����� ������

    private void Awake()
    {
        // �������� ��������� �� ������ ��������� �������
        playerController = GetComponent<HeroController>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    //private void Update()
    //{
    //    HandleWalkSound();
    //}

    //private void HandleWalkSound()
    //{
    //    if (playerController.IsWalking()) // ���� ������� ������
    //    {
    //        walkTimer -= Time.deltaTime;

    //        if (walkTimer <= 0f)
    //        {
    //            PlaySound(walkSound);
    //            walkTimer = walkSoundInterval; // ������� ������
    //        }
    //    }
    //    else
    //    {
    //        walkTimer = 0f; // ������� ������, ���� ������� �������� ������
    //    }
    //}

    public void PlaySwordSwingSound()
    {
        PlaySound(swordSwingSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // ³��������� ����
        }
    }
}
