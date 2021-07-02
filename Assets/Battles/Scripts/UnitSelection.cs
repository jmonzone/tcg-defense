using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private LayerMask unitLayer;
    private UnitView selectedUnit;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;

        var origin = cam.ScreenToWorldPoint(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(origin, Vector2.zero, Mathf.Infinity, unitLayer);
            if (hit.collider != null)
            {
                var unit = hit.transform.GetComponentInParent<UnitView>();
                if (!unit) return;

                if (!selectedUnit) SelectUnit(unit);
                else if (selectedUnit != unit)
                {
                    UnselectUnit();
                    SelectUnit(unit);
                }

            }
            else if (selectedUnit)
            {
                UnselectUnit();
            }
        }
        
    }

    private void SelectUnit(UnitView unit)
    {
        selectedUnit = unit;

        foreach (var selectable in unit.GetComponents<ISelectable>())
        {
            selectable.OnSelect();
        }
    }

    private void UnselectUnit()
    {
        foreach (var selectable in selectedUnit.GetComponents<ISelectable>())
        {
            selectable.OnUnselect();
        }

        selectedUnit = null;

    }
}
