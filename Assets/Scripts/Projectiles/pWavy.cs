using UnityEngine;
using System.Collections;
using System;

public class pWavy : Projectile {

    public float Amplitude;
    public float WaveLength;

    private float _creationTime;

    protected override void Start()
    {
        base.Start();

        _creationTime = Time.timeSinceLevelLoad;
    }

    protected override void Move()
    {
        if (Vector2.Distance(_startLoc, transform.position) >= MaxDistance)
            Destroy(gameObject);
        else
        {
            Vector2 newPos = transform.position;
            Vector2 right = transform.right * Speed * Time.deltaTime;
            newPos.x += right.x;
            newPos.y += right.y;

            Vector2 wave = CalculateWave();
            newPos.x += wave.x;
            newPos.y += wave.y;

            _rigid.MovePosition(newPos);
        }
    }

    private Vector2 CalculateWave()
    {
        Vector2 wave = transform.up * (Amplitude * Mathf.Sin(WaveLength * TimeSinceInit()));




        return wave;
    }

    private float TimeSinceInit()
    {
        return Time.timeSinceLevelLoad - _creationTime;
    }
}
