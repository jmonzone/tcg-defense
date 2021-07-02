using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEView : MonoBehaviour
{
    [SerializeField] private float duration;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Spawn(Vector2 position, float radius)
    {
        transform.localScale = Vector3.one * radius * 2;
        transform.position = position;
        gameObject.SetActive(true);
        StartCoroutine(DurationCoroutine());
    }

    private IEnumerator DurationCoroutine()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
    
}
