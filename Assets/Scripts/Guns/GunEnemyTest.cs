using UnityEngine;
using System.Collections;

public class GunEnemyTest : Gun {

    public override GameObject Fire()
    {
        return (GameObject)Instantiate(Projectile, transform.position, new Quaternion(0,0,-90,0));
    }
}
