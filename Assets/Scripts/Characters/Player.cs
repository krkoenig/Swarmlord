using UnityEngine;
using System.Collections;

public class Player : Character
{
    protected override void Movement()
    {
        Vector2 newPos = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Speed * Time.deltaTime;
        newPos.x += transform.position.x;
        newPos.y += transform.position.y;
        _rigid.MovePosition(newPos);
    }

    protected override void Fire()
    {
        if (Input.GetAxis("Fire1") > 0 && _fireTimer.ElapsedMilliseconds > 1000.0f)
        {
            _fireTimer.Reset();
            _fireTimer.Start();

            GameObject p = _gun.Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            p.SendMessage("SetOwner", gameObject);
        }
    }

}
