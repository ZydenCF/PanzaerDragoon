using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private float speed = 10.0f;
    private int health;
    private Rigidbody rb;
    public GameObject projectilePrefab;
    private float fireRate = 0.3f;
    private float fireTimer = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 100;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 1) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void HandleShooting()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer >= fireRate)
        {
            fireTimer = 0.0f;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.Initialize(transform.forward, 20, true);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UIManager.Instance.UpdateHealth(health);

        if (health <= 0)
        {
            GameManager.Instance.OnPlayerDeadEvent();
            Destroy(gameObject);
        }
    }
}
