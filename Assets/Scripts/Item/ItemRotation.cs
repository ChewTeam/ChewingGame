using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemMovement))]
public class ItemRotation : MonoBehaviour
{
    private ItemMovement _itemMovement;

    private void Start()
    {
        _itemMovement = GetComponent<ItemMovement>();
    }

    private void Update()
    {
        if (_itemMovement.IsMoving != false)
        transform.rotation = Quaternion.LookRotation(_itemMovement.TargetPoint.position - transform.position, Vector3.forward);
    }
}
