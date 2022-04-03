using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _filter;
    private Tooth _selectedTooth;
    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0))
        {
            if (_selectedTooth == null)
            {
                if (Physics.Raycast(mouseRay, out hit, _filter))
                {
                    if (hit.collider.gameObject.TryGetComponent(out Tooth tooth))
                    {
                        _selectedTooth = tooth;
                        _selectedTooth.Enter();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_selectedTooth != null)
            {
                _selectedTooth.Exit();
                _selectedTooth = null;
            }
        }
    }
}
