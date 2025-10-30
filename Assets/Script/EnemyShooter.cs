using UnityEngine;

public class EnemyShooter : EnemyBase
{
    private float fireRate = 2.0f;
    private float fireTimer = 0.0f;
    public GameObject projectilePrefab;

    public override void Move()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    public override void Attack()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0.0f;
            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = newProjectile.GetComponent<Projectile>();
            projectile.Initialize(Vector3.back, 10, false);
        }
    }
}
