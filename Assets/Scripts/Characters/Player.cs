using UnityEngine;
using System.Collections;

public class Player : Character
{

    protected override void Movement()
    {
        Vector2 newPos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Speed * Time.deltaTime;
        newPos.x += transform.position.x;
        newPos.y += transform.position.y;

        newPos.x = Mathf.Clamp(newPos.x, _minX, _maxX);
        newPos.y = Mathf.Clamp(newPos.y, _minY, _maxY);

        _rigid.MovePosition(newPos);
    }

    protected override void Fire()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            GameObject p = _gun.Fire();
            p.tag = "PlayerProjectile";
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        print("Collision!");

        if (coll.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            coll.gameObject.SendMessage("TakeDamage", 1000);

            TakeDamage(10);
        }
    }
}
