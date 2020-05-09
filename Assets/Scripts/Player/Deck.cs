using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public Stack<Card> deck;
    public Stack<Card> discard;
    public Hand hand;

    public Text deckSizeText;
    public Text discardSizeText;

    private void Start() {
        discard = new Stack<Card>();
        hand.deck = this;
    }

    public void Deal(int numCards){

        if (deck.Count < numCards){
            numCards -= deck.Count;
            Deal(deck.Count);
            ShuffleDiscardIntoDeck();
            Deal(numCards);
            return;
        }

        for (int i = 0; i < numCards; i++)
        {
            if (hand.GetHandSize() >= hand.maxHandSize){
                AddToDiscard(deck.Pop());
            }

            hand.Draw(deck.Pop());
        }

        deckSizeText.text = deck.Count.ToString();
    }

    public void AddToDiscard(Card card){
        card.gameObject.SetActive(false);
        card.played = false;
        card.AdjustTransforms(transform);
        discard.Push(card);
        discardSizeText.text = discard.Count.ToString();
    }

    public void ShuffleDiscardIntoDeck(){

        int numCards = discard.Count;

        for (int i = 0; i < numCards; i++){
            deck.Push(discard.Pop());
        }

        Shuffle();

        discardSizeText.text = discard.Count.ToString();
        deckSizeText.text = deck.Count.ToString();
    }

    public void Shuffle(){
        Card[] tempDeck = deck.ToArray();
        deck.Clear();
        
        for (int i = 0; i < tempDeck.Length; i++){
            Card tempCard = tempDeck[i];
            int r = Random.Range(0, i);
            tempDeck[i] = tempDeck[r];
            tempDeck[r] = tempCard;
        }

        for (int i = 0; i < tempDeck.Length; i++){
            deck.Push(tempDeck[i]);
        }
    }
}
