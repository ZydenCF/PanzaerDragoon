using UnityEngine;

public class MiniBoss : EnemyBase
{
    private float fireRate = 1.0f;
    private float fireTimer = 0.0f;
    public GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        health = 200;
        speed = 2.0f;
    }

    public override void Move()
    {
        // Avanzar hacia adelante automáticamente
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public override void Attack()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0.0f;

            // Disparar 3 proyectiles en abanico hacia el jugador
            for (int i = -1; i <= 1; i++)
            {
                GameObject newProjectile = Instantiate(projectilePrefab,
                    transform.position, Quaternion.identity);
                Projectile projectile = newProjectile.GetComponent<Projectile>();

                // Calcular dirección hacia el jugador con variación
                if (target != null)
                {
                    Vector3 directionToPlayer = (target.position - transform.position).normalized;
                    Quaternion spread = Quaternion.Euler(0, i * 15, 0);
                    Vector3 finalDirection = spread * directionToPlayer;
                    projectile.Initialize(finalDirection, 15, false);
                }
            }
        }
    }

    protected override void Die()
    {
        GameManager.Instance.OnBossDefeatedEvent();
        Destroy(gameObject);
    }
}