using System;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image background;
    [SerializeField] private Image image;
    [SerializeField] private Text cardName;
    [SerializeField] private Button button;

    public void UpdateView(Card card, Action onClick)
    {
        gameObject.SetActive(true);
        background.color = card.Color;
        image.sprite = card.sprite;
        cardName.text = card.name;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
