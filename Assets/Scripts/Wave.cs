using UnityEngine;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
    public float Delay;

    private List<GameObject> _waveList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        print("Enemies found: " + transform.childCount);


        for (int i = 0; i < transform.childCount; i++)
        {
            print(transform.GetChild(i).name);
            _waveList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject g in _waveList)
        {
            g.SetActive(false);
        }
    }

    public void Activate()
    {
        foreach (GameObject g in _waveList)
        {
            g.SetActive(true);
        }
    }
}
