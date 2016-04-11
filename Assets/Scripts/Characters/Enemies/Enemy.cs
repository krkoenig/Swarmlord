using UnityEngine;
using System.Collections;

public class Enemy : Character {

    public float FireDelay;
    public float FollowDistance;

    private Transform _playerTransform;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Movement()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) >= FollowDistance)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, _playerTransform.position, Speed * Time.deltaTime);
            _rigid.MovePosition(newPos);

        }
    }

    protected override void Fire()
    {
        if (_fireTimer.ElapsedMilliseconds / 1000 >= FireDelay)
        {
            _fireTimer.Reset();
            _fireTimer.Start();

            GameObject p = _gun.Fire(_playerTransform.position);
            p.SendMessage("SetOwner", gameObject);
        }
    }
}
