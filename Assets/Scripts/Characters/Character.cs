using UnityEngine;
using System.Collections;
using System.Diagnostics;

public abstract class Character : MonoBehaviour
{

    public float Health;
    public float Speed;

    protected Gun _gun;
    protected Stopwatch _fireTimer = new Stopwatch();
    protected Rigidbody2D _rigid;

    // Use this for initialization
    protected virtual void Start()
    {
        _gun = GetComponentInChildren<Gun>();
        _fireTimer.Start();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Fire();
    }

    protected virtual void FixedUpdate()
    {
        Movement();
    }

    protected abstract void Movement();

    protected abstract void Fire();

    protected void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
            Die();
    }

    protected void Die()
    {
        print(gameObject.name + " has died!");
        Destroy(gameObject);
    }
}
