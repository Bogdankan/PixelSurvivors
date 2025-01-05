using UnityEngine;

public class Damaged : MonoBehaviour
{
    public int damage = 10; 

    public void ApplyDamage(GameObject target)
    {
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            health.ChangeHealth(-1);
        }
    }
}
