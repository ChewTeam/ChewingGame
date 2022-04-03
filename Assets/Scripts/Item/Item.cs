using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ItemMovement))]
abstract public class Item : MonoBehaviour
{
    [SerializeField] private float _delayDisable;
    [SerializeField] private ParticleSystem _pathEffect;
    [SerializeField] private string _label;

    private Rigidbody _rigidbody;

    public UnityEvent HitTooth;

    public UnityAction<bool> Disabled;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Disable()
    {
        _pathEffect.gameObject.SetActive(true);
        Disabled?.Invoke(true);
        _rigidbody.isKinematic = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tooth tooth))
        {
            _pathEffect.gameObject.SetActive(false);
            HitTooth?.Invoke();
            Disabled?.Invoke(false);
            _rigidbody.isKinematic = false;

            Invoke(nameof(Disable), _delayDisable);
        }
    }
}

