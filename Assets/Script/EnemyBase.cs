using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    protected int health;
    protected float speed;
    protected Transform target;


    protected virtual void Start()
    {
        health = 50;
        speed = 3.0f;
        target = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        Move();
        Attack();
    }

    public abstract void Move();
    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GameManager.Instance.OnEnemyDeadEvent(this);
        Destroy(gameObject);
    }
}
