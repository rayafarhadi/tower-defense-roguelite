using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{

    public float cardWidth = 140f;
    public Card[] hand;
    public Deck deck;
    public static Card activeCard;

    public int maxHandSize = 9;
    private int handSize = 0;

    // handLength = (cardWidth/2) + (cardWidth/2)*cards.Length;
    // cardInterval = handLength / cards.Length;
    // cardPos = (-handLength/2) + cardInterval*i;
    
    private void Start() {
        hand = new Card[9];
    }

    private void Update() {
        OrganizeHand();
    }

    private void OrganizeHand(){
        float cardInterval = cardWidth/2;
        float handAreaLength = cardInterval + cardInterval*handSize;

        for (int i = 0; i < handSize; i++)
        {
            float cardX = (-handAreaLength/2) + cardInterval*(i+1);
            hand[i].transform.localPosition = new Vector3(cardX, 0f, 0f);
        }
    }

    public void Draw(Card card){
        card.gameObject.SetActive(true);
        card.hand = this;
        hand[handSize] = card;
        hand[handSize].AdjustTransforms(transform);
        handSize++;
    }

    public void Discard(Card card){
        int discardIndex = 0;

        for (int i = 0; i < handSize; i++){
            if (hand[i].Equals(card)){
                discardIndex = i;
                break;
            }
        }

        for(int i = discardIndex+1; i < handSize; i++){
            hand[i-1] = hand[i];
        }

        handSize--;
        hand[handSize] = null;

        card.gameObject.SetActive(false);
        deck.AddToDiscard(card);
    }

    public int GetHandSize(){
        return handSize;
    }
}
