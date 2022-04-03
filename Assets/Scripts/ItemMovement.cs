using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _delayDisable;

    private Rigidbody _rigidbody;
    private Transform _targetPointStorage;
    private Transform _exitPoint;
    private Transform _targetPoints;
    private const string _targetStorageTag = "TargetPointStorage";
    private const string _tagPointExit = "ExitPoint";

    public UnityEvent Swallowed;
    public UnityEvent HitTooth;
    public bool IsMoving { get; private set; }

    public void SpecifyTargetPoint()
    {
        int randomPoint = Random.Range(0, _targetPointStorage.childCount);
        _targetPoints = _targetPointStorage.GetChild(randomPoint);
    }

    public void SpecifyTargetPoint(Transform targetPoint)
    {
        _targetPoints = targetPoint;
    }

    private void Start()
    {
        IsMoving = true;
        _rigidbody = GetComponent<Rigidbody>();
        _targetPointStorage = GameObject.FindGameObjectWithTag(_targetStorageTag).transform;
        _exitPoint = GameObject.FindGameObjectWithTag(_tagPointExit).transform;

        SpecifyTargetPoint();
    }

    private void Update()
    {
        if (IsMoving != false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoints.position, _moveSpeed * Time.deltaTime);
            if (transform.position == _targetPoints.position)
            {
                SpecifyTargetPoint(_exitPoint);
            }
            if (transform.position == _exitPoint.position)
            {
                Swallowed?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tooth tooth))
        {
            HitTooth?.Invoke();
            IsMoving = false;
            _rigidbody.isKinematic = false;

            StartCoroutine(DisableItemWithDelay());
        }
    }

    private IEnumerator DisableItemWithDelay()
    {
        yield return new WaitForSeconds(_delayDisable);
        IsMoving = true;
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
    }
}
