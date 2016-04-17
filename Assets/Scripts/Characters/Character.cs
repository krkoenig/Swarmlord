using UnityEngine;
using System.Collections;
using System.Diagnostics;

public abstract class Character : MonoBehaviour
{
    public float Health;
    public float Speed;

    protected Gun _gun;
    protected Rigidbody2D _rigid;
    protected float _minX, _minY, _maxX, _maxY;
    protected Stopwatch _fireTimer = new Stopwatch();


    // Use this for initialization
    protected virtual void Start()
    {
        _gun = GetComponentInChildren<Gun>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _fireTimer.Start();

        CalculateBounds();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if((float)_fireTimer.ElapsedMilliseconds / 1000 >= _gun.FireDelay)
        {
            Fire();
            _fireTimer.Reset();
            _fireTimer.Start();
        }
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

    protected virtual void Die()
    {
        print(gameObject.name + " has died!");
        Destroy(gameObject);
    }

    protected void CalculateBounds()
    {
        Vector3 spriteBoundSize = gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size;

        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        _minX = (float)(spriteBoundSize.x / 2.0f - horzExtent);
        _maxX = (float)(horzExtent - spriteBoundSize.x / 2.0f);
        _minY = (float)(spriteBoundSize.y / 2.0f - vertExtent);
        _maxY = (float)(vertExtent - spriteBoundSize.y / 2.0f);
    }
}
