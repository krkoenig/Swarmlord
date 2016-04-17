using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour
{
    public GameObject Projectile;
    public float FireDelay;

    public abstract GameObject Fire();
}
