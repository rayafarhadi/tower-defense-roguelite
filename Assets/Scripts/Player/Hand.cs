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
    public int focusedCardIndex = -1;

    public int maxHandSize = 9;
    private int handSize = 0;
    
    private void Awake() {
        hand = new Card[9];
    }

    public void OrganizeHand(){
        float cardInterval = cardWidth/2;
        float handAreaLength = cardInterval + cardInterval*handSize;

        for (int i = 0; i < handSize; i++)
        {
            float cardX = (-handAreaLength/2) + cardInterval*(i+1);
            hand[i].transform.localPosition = new Vector3(cardX, -50f, 0f);
            hand[i].handPosition = i;
            if(activeCard != null){
                if (i != activeCard.handPosition){
                    hand[i].activeIndicator.enabled = false;
                }
            } else {
                hand[i].activeIndicator.enabled = false;
            }
            
        }

        if (focusedCardIndex >= 0){
            Debug.Log("focused");
            for (int i = 0; i < focusedCardIndex; i++){
                Transform cardTransform = hand[i].transform;
                cardTransform.Translate(new Vector3(-cardWidth/2, 0f, 0f), transform);
            }

            for (int i = focusedCardIndex + 1; i < handSize; i++){
                Transform cardTransform = hand[i].transform;
                cardTransform.Translate(new Vector3(cardWidth/2, 0f, 0f));
            }
        }
    }

    public void Draw(Card card){
        card.gameObject.SetActive(true);
        card.hand = this;
        hand[handSize] = card;
        hand[handSize].AdjustTransforms(transform);
        handSize++;
        OrganizeHand();
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

        deck.AddToDiscard(card);
        OrganizeHand();
    }

    public void DiscardHand(){
        for(int i = 0; i < handSize; i++){
            deck.AddToDiscard(hand[i]);
            hand[i] = null;
        }

        handSize = 0;
    }

    public int GetHandSize(){
        return handSize;
    }
}
