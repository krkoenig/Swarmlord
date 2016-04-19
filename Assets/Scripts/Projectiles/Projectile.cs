using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {

    public float Speed;
    public float MaxDistance;
    public float Damage;

    protected Vector2 _startLoc;
    protected Rigidbody2D _rigid;

    // Use this for initialization
    protected virtual void Start () {
        _startLoc = transform.position;
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update () {
        Move();      
	}  

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.layer == LayerMask.NameToLayer("Player") && CompareTag("EnemyProjectile")) ||
            (coll.gameObject.layer == LayerMask.NameToLayer("Enemy") && CompareTag("PlayerProjectile")))
        {
            coll.gameObject.SendMessage("TakeDamage", Damage);

            if (coll.gameObject.layer != LayerMask.NameToLayer("Projectile"))
            {
                Destroy(gameObject);
            }
        }    
    }

    protected abstract void Move();
}
