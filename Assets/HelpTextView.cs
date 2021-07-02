using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTextView : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private DeckManager deck;

    private void Start()
    {
        deck.OnCardSelected += () => Toggle(true);
        deck.OnCardCanceled += () => Toggle(true);
        deck.OnCardPlayed += () => Toggle(false);
    }

    private void Toggle(bool value)
    {
        text.enabled = value;
    }
}
