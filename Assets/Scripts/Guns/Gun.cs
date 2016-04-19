using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    public GameObject Projectile;
    public float FireDelay;
    public bool PlayerGun;

    protected string _tag;

    protected virtual void Start()
    {
        _tag = PlayerGun ? "PlayerProjectile" : "EnemyProjectile";
    }

    public abstract void Fire();
}
