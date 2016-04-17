using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour {

    public float Speed;

    private float _position = 0;
    private Renderer _renderer;

    void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        _position += Speed;
        if (_position > 1.0f)
            _position--;

        _renderer.material.mainTextureOffset = new Vector2(_position, 0);
    }
}
