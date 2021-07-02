using System;
using System.Collections;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask placeableLayer;
    [SerializeField] private Color validColor;
    [SerializeField] private Color restrictedColor;

    private bool isPlaced = false;
    public bool IsPlaced
    {
        get => isPlaced;
        private set
        {
            isPlaced = value;
            OnPlacementStateChanged?.Invoke();
        }
    }

    public event Action OnPlacementStateChanged;

    public void StartPlacement(Action<RaycastHit2D> onComplete, Action onCancel, Action<RaycastHit2D> onPlaceable = null) 
    {
        StartCoroutine(PlacementCoroutine(onComplete, onCancel, onPlaceable));
    }

    private IEnumerator PlacementCoroutine(Action<RaycastHit2D> onComplete, Action onCancel, Action<RaycastHit2D> onPlaceable)
    {
        var cam = Camera.main;
        while (true)
        {
            var mousePos = Input.mousePosition;
            var origin = cam.ScreenToWorldPoint(mousePos);
            origin.z = 0;

            var hit = Physics2D.Raycast(origin, Vector2.zero, Mathf.Infinity, groundLayer);

            if (hit.collider != null)
            {
                transform.position = origin;
            }

            hit = Physics2D.Raycast(origin, Vector2.zero, Mathf.Infinity, placeableLayer);

            if (hit.collider != null)
            {
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.color = validColor;
                }

                onPlaceable?.Invoke(hit);

                if (Input.GetMouseButtonDown(0))
                {
                    onComplete?.Invoke(hit);
                    break;
                }
            }
            else
            {
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.material.color = restrictedColor;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                onCancel?.Invoke();
                break;
            }

            yield return null;
        }
    }
}
