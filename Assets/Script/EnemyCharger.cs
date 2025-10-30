using UnityEngine;

public class EnemyCharger : EnemyBase
{
    [SerializeField] private int collisionDamage = 30;
    [SerializeField] private float chargeDistance = 15f;
    [SerializeField] private float chargeMultiplier = 3f;
    [SerializeField] private float normalSpeedMultiplier = 0.5f;

    private bool charging = false;

    public override void Move()
    {
        
        float moveSpeed = speed * (charging ? chargeMultiplier : normalSpeedMultiplier);
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
        
    }

    public override void Attack()
    {
        if (target == null) return;

        
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < chargeDistance)
        {
            charging = true;
        }
        else
        {
            charging = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable player = other.GetComponent<IDamageable>();
            if (player != null)
            {
                player.TakeDamage(collisionDamage);
            }

            
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            if (player != null)
            {
                player.TakeDamage(collisionDamage);
            }

           
            Destroy(gameObject);
        }
    }
}