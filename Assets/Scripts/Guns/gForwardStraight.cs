using UnityEngine;
using System.Collections;

public class gForwardStraight : Gun
{

    private float _dir;

    protected override void Start()
    {
        base.Start();

        _dir = PlayerGun ? 0.0f : 180.0f;
    }

    public override void Fire()
    {
        GameObject g = (GameObject)Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, _dir));
        g.tag = _tag;
    }
}
