using UnityEngine;
using System.Collections;

/// <summary>
/// A Beam behaves similar to a regular Straight projectile, but
/// ignores collision and continues till it hits its maximum distance.
/// </summary>
public class pBeam : Projectile
{

    protected override void Move()
    {
        if (Vector2.Distance(_startLoc, transform.position) >= MaxDistance)
            Destroy(gameObject);
        else
        {
            Vector2 newPos = transform.position;
            Vector2 right = transform.right * Speed * Time.deltaTime;
            newPos.x += right.x;
            newPos.y += right.y;

            _rigid.MovePosition(newPos);
        }
    }

    /// <summary>
    /// Overrides base Projectile OnTriggerEnter2D and does nothing.
    /// Beams hit for as long as an object is inside of it, see
    /// OnTriggerStay2D.
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnTriggerEnter2D(Collider2D coll) { }

    void OnTriggerStay2D(Collider2D coll)
    {
        if ((coll.gameObject.layer == LayerMask.NameToLayer("Player") && CompareTag("EnemyProjectile")) ||
            (coll.gameObject.layer == LayerMask.NameToLayer("Enemy") && CompareTag("PlayerProjectile")))
        {
            coll.gameObject.SendMessage("TakeDamage", Damage);
        }
    }
}
