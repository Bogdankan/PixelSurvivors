using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Джерело звуку
    [SerializeField] private AudioClip walkSound; // Звук ходьби
    [SerializeField] private AudioClip swordSwingSound; // Звук удару мечем
    [SerializeField] private float walkSoundInterval = 0.5f; // Інтервал між звуками ходьби

    private HeroController playerController; // Скрипт, що керує гравцем
    private float walkTimer; // Таймер для звуку ходьби

    private void Awake()
    {
        // Отримуємо посилання на скрипт керування гравцем
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
    //    if (playerController.IsWalking()) // Якщо гравець ходить
    //    {
    //        walkTimer -= Time.deltaTime;

    //        if (walkTimer <= 0f)
    //        {
    //            PlaySound(walkSound);
    //            walkTimer = walkSoundInterval; // Скидаємо таймер
    //        }
    //    }
    //    else
    //    {
    //        walkTimer = 0f; // Скидаємо таймер, якщо гравець перестав ходити
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
            audioSource.PlayOneShot(clip); // Відтворюємо звук
        }
    }
}
