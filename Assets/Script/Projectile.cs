using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private int damage;
    private float speed = 25.0f;
    private bool fromPlayer;
    private Vector3 startPosition;
    private float maxDistance = 50f; // Límite de distancia

    public void Initialize(Vector3 dir, int dmg, bool playerOwned)
    {
        direction = dir;
        damage = dmg;
        fromPlayer = playerOwned;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Destruir si supera la distancia máxima
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null)
        {
            if (fromPlayer && other.CompareTag("Enemy"))
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (!fromPlayer && other.CompareTag("Player"))
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}