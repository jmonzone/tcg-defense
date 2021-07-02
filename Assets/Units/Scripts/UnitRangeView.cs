using System;
using UnityEngine;
using UnityEngine.UI;

public interface ISelectable
{
    public void OnSelect();
    public void OnUnselect();
}

public class UnitBehaviour : MonoBehaviour
{
    public UnitView Unit { get; private set; }

    protected virtual void Start()
    {
        Unit = GetComponent<UnitView>();
    }
}
public class UnitRangeView : UnitBehaviour, ISelectable
{
    [SerializeField] private Transform indicator;
    [SerializeField] private PlaceableObject unitPlacement;

    protected override void Start()
    {
        base.Start();
        Unit.OnRangeChanged += UpdateView;
        UpdateView();
    }

    public void OnSelect()
    {
        indicator.gameObject.SetActive(true);
    }
    public void OnUnselect()
    {
        indicator.gameObject.SetActive(false);
    }

    private void UpdateView()
    {
        indicator.localScale = Vector3.one * Unit.Range * 2;
    }
}
