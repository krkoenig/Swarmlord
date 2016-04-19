using UnityEngine;
using System.Collections;

public class gTripleSplit : Gun {

    private float _first, _second, _third;

    protected override void Start()
    {
        base.Start();

        _first = PlayerGun ? 45.0f : 225.0f;
        _second = PlayerGun ? 0.0f : 180.0f;
        _third = PlayerGun ? -45.0f : 135.0f;

    }

    public override void Fire()
    {
        GameObject g1 = (GameObject)Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, _first));
        GameObject g2 = (GameObject)Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, _second));
        GameObject g3 = (GameObject)Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, _third));

        g1.tag = _tag;
        g2.tag = _tag;
        g3.tag = _tag;
    }
}
