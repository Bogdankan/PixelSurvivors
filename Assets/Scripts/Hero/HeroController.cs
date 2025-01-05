using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    public float speed = 5f;
    //public event Action onLose;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    [SerializeField] private GameObject[] weapons;
    private int currentWeaponIndex = 0; // Поточний індекс зброї
    private DependencyManager dependencyManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dependencyManager = FindObjectOfType<DependencyManager>(); // Знаходимо GameManager
        EquipWeapon(currentWeaponIndex); // Активуємо початкову зброю
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetBool("isMoving", movement != Vector2.zero);

        if (Input.GetKeyDown(KeyCode.C)) // Зміна зброї натисканням C
        {
            ChangeWeapon();
        }
    }

    public void ChangeWeapon()
    {
        if (weapons.Length == 0 || weapons == null) return;
        // Деактивуємо поточну зброю
        weapons[currentWeaponIndex].SetActive(false);

        // Переходимо до наступної зброї
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

        // Активуємо нову зброю
        EquipWeapon(currentWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weapons.Length == 0 || weapons == null) return;
        GameObject weapon = weapons[weaponIndex];
        weapon.SetActive(true);

        WeaponController weaponController = weapon.GetComponent<WeaponController>();
        if (weaponController != null)
        {
            if (weapon.name.ToLower().Contains("bow"))
            {
                dependencyManager.InitializeBow(weaponController);
            }
            else if (weapon.name.ToLower().Contains("sword"))
            {
                dependencyManager.InitializeSword(weaponController);
            }
        }
    }   

    private void OnDisable()
    {
        // Перевіряємо, чи сцена вже змінюється
        if (SceneManager.GetActiveScene().name != "Intro" && !SceneManager.GetSceneByName("Intro").isLoaded)
        {
            Debug.Log("Intro");
            SceneManager.LoadScene("Intro"); // Завантажуємо головне меню
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
