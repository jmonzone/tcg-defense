using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private DeckManager deck;
    [SerializeField] private RoundManager roundManager;

    private List<CardView> cards = new List<CardView>();

    private void Start()
    {
        roundManager.OnRoundEnded += UpdateView;


        GetComponentsInChildren(cards);
        UpdateView();
    }

    private void UpdateView()
    {
        for (var i = 0; i < DeckManager.DEFAULT_MAX_HAND; i++)
        {
            var card = deck.Hand[i];
            var view = cards[i];

            view.UpdateView(card, () =>
            {
                deck.SelectCard(card, onComplete: () =>
                {
                    ToggleCards(true);
                },
                onCancel: () =>
                {
                    view.SetActive(true);
                    ToggleCards(true);
                });
                view.SetActive(false);
                ToggleCards(false);
            });
        }
    }

    private void ToggleCards(bool active)
    {
        cards.ForEach((view) =>
        {
            view.SetInteractable(active);
        });
    }
}
