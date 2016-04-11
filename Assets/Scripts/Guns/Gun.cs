using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject Projectile;

    public GameObject Fire(Vector2 target)
    { 
        return (GameObject)Instantiate(Projectile, transform.position, AngleBetween(target));
    }

    private Quaternion AngleBetween(Vector2 target)
    {
        float rad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
        float degree = Mathf.Rad2Deg * rad;

        return Quaternion.Euler(0, 0, degree);
    }
}
