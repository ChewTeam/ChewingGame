using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Item))]
public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Transform _targetPointStorage;
    private Transform _exitPoint;
    private Transform _targetPoint;
    private Item _item;

    public UnityEvent Swallowed;
    public bool IsMoving { get; private set; }
    public Transform TargetPoint => _targetPoint;

    public void SpecifyTargetPoint()
    {
        int randomPoint = Random.Range(0, _targetPointStorage.childCount);
        _targetPoint = _targetPointStorage.GetChild(randomPoint);
    }

    public void SpecifyTargetPoint(Transform targetPoint)
    {
        _targetPoint = targetPoint;
    }

    public void SetExitPoint(Transform exitPoint)
    {
        _exitPoint = exitPoint;
    }
    
    public void SetTargetPointStorage(Transform targetPointStorage)
    {
        _targetPointStorage = targetPointStorage;
    }

    private void Awake()
    {
        _item = GetComponent<Item>();
    }

    private void Start()
    {
        IsMoving = true;
        SpecifyTargetPoint();
    }

    private void Update()
    {
        if (IsMoving != false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _moveSpeed * Time.deltaTime);
            if (transform.position == _targetPoint.position)
            {
                SpecifyTargetPoint(_exitPoint);
            }
            if (transform.position == _exitPoint.position)
            {
                Swallowed?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        _item.Disabled += OnDisabled;
    }

    private void OnDisable()
    {
        _item.Disabled -= OnDisabled;
    }

    private void OnDisabled(bool disable)
    {
        IsMoving = disable;
    }
}
