using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooth : MonoBehaviour
{
    [SerializeField] private float _minPositionY;
    [SerializeField] private float _maxPositionY;

    [SerializeField] private bool _isSelected;

    private float _newPositionY;
    private float _oldPositonY;

    private void Start()
    {
        _newPositionY = transform.position.y;
    }

    private void Update()
    {
        if (_isSelected)
        {
            Move();
        }
    }

    public void Enter()
    {
        _isSelected = true;
    }

    public void Exit()
    {
        _isSelected = false;
    }

    private void Move()
    {
        float mousePositionY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetMouseButtonDown(0))
        {
            _newPositionY = mousePositionY;
            _oldPositonY = transform.position.y;
        }

        transform.position = new Vector3(transform.position.x, _oldPositonY + mousePositionY - _newPositionY, transform.position.z);
        float correctPositionY = Mathf.Clamp(transform.position.y, _minPositionY, _maxPositionY);
        transform.position = new Vector3(transform.position.x, correctPositionY, transform.position.z);
    }
}
