using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private float _delayDisable;
}
