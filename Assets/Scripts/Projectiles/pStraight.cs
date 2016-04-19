using UnityEngine;
using System.Collections;
using System;

public class pStraight : Projectile {

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
}
