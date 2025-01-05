using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform _target;      
    public float speed = 1.5f;        
    public float stopDistance = 1.0f; 

    private Animator animator;        
    private Rigidbody2D rb;          
    private Vector2 movement;         

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (_target == null)
        {
            Debug.LogError("Ціль не задано для EnemyController!");
        }
    }
    
    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector2 direction = (_target.position - transform.position).normalized;

            float distance = Vector2.Distance(transform.position, _target.position);

            if (distance > stopDistance)
            {
                movement = direction;
                //Debug.Log(movement);
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

                animator.SetFloat("horizontal", movement.x);
                animator.SetFloat("vertical", movement.y);
                animator.SetBool("isMoving", movement != Vector2.zero);
            }
            else
            {
                movement = Vector2.zero;
                animator.SetBool("isMoving", movement != Vector2.zero);
            }
        }
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
