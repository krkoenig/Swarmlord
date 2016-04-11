using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float Speed;
    public float MaxDistance;
    public float Damage;

    private Vector2 _startLoc;
    private GameObject _owner;
    protected Rigidbody2D _rigid;

    // Use this for initialization
    void Start () {
        _startLoc = transform.position;
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        print("Fired!");

        if (coll.gameObject.layer == LayerMask.NameToLayer("Character") && coll.gameObject != _owner)
        {
            coll.gameObject.SendMessage("TakeDamage", Damage);

            if (coll.gameObject.layer != LayerMask.NameToLayer("Projectile"))
            {
                Destroy(gameObject);
            }
        }    
    }

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }
}
