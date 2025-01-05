using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SwordController : MonoBehaviour
{
    public float swingDistance = 0.5f;
    public float swingDuration = 0.2f; 
    public float attackCooldown = 0.5f;

    [SerializeField] private AudioSource audioSource; // Джерело звуку
    //[SerializeField] private AudioClip walkSound; // Звук ходьби
    [SerializeField] private AudioClip swordSwingSound; // Звук удару мечем
    //[SerializeField] private float walkSoundInterval = 0.5f; // Інтервал між звуками ходьби

    private Vector3 initialPosition;
    private Damaged damaged; 
    private Coroutine attackCoroutine; 

    void Start()
    {
        initialPosition = transform.localPosition;
        damaged = GetComponent<Damaged>();
        if (damaged == null)
        {
            Debug.LogError("Damaged компонент не знайдено на мечі!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && attackCoroutine == null && other.gameObject.activeInHierarchy)
        {
            PlaySwordSwingSound();
            attackCoroutine = StartCoroutine(AttackEnemy(other));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private IEnumerator AttackEnemy(Collider2D target)
    {
        while (true)
        {
            if (target == null || !target.gameObject.activeInHierarchy)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
                yield break;
            }

            damaged.ApplyDamage(target.gameObject);
            

            yield return StartCoroutine(SwingSword(target.transform));

            yield return new WaitForSeconds(attackCooldown);
        }
    }

    private IEnumerator SwingSword(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Vector3 targetPosition = initialPosition + direction * swingDistance;
        float elapsedTime = 0f;

        while (elapsedTime < swingDuration)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        elapsedTime = 0f;

        while (elapsedTime < swingDuration)
        {
            transform.localPosition = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }

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
