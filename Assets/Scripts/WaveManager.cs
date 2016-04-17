using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

public class WaveManager : MonoBehaviour
{
    private List<Wave> _waves = new List<Wave>();
    private Stopwatch _waveTimer = new Stopwatch();
    private int _currentWave = 0;

    // Use this for initialization
    void Start()
    {
        print("Waves found: " + transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            _waves.Add(transform.GetChild(i).gameObject.GetComponent<Wave>());
        }

        _waveTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWave < _waves.Count)
        {
            if ((float)_waveTimer.ElapsedMilliseconds / 1000 >= _waves[_currentWave].Delay)
            {
                _waves[_currentWave].Activate();
                _currentWave++;

                _waveTimer.Reset();
                _waveTimer.Start();
            }
        }
    }
}