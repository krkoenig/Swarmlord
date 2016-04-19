using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Enemy : Character
{

    public Path Path;

    protected override void Movement()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, Path.CurrentPoint.transform.position, Speed * Time.deltaTime);
        _rigid.MovePosition(newPos);
    }

    protected override void Fire()
    {
        _gun.Fire();
    }

    protected override void Die()
    {
        print(transform.parent.gameObject.name + " has died!");
        Destroy(transform.parent.gameObject);
    }
}
