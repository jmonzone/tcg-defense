using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CardAmount
{
    public Card card;
    public int amount;
}

[Serializable]
public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    public Deck(List<CardAmount> cardAmounts)
    {
        cardAmounts.ForEach((card) =>
        {
            for (var i = 0; i < card.amount; i++)
            {
                Cards.Add(card.card);
            }
        });

        Cards.Shuffle();
    }
}

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<CardAmount> serializeableDeck;

    public const int DEFAULT_MAX_HAND = 5;

    public Deck Deck { get; private set; }
    public List<Card> Hand => Deck.Cards.GetRange(0, DEFAULT_MAX_HAND);

    public event Action OnCardSelected;
    public event Action OnCardCanceled;
    public event Action OnCardPlayed;

    private void Awake()
    {
        Deck = new Deck(serializeableDeck);
    }

    public void SelectCard(Card card, Action onComplete, Action onCancel)
    {
        void OnCancel()
        {
            onCancel?.Invoke();
            OnCardCanceled?.Invoke();
        }

        card.Select(onComplete: () =>
        {
            PlayCard(card);
            onComplete?.Invoke();
        }, OnCancel);

        OnCardSelected?.Invoke();
    }

    private void PlayCard(Card card)
    {
        OnCardPlayed?.Invoke();
        Deck.Cards.Remove(card);
        Deck.Cards.Insert(Deck.Cards.Count, card);
    }
}
