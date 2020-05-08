using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{

    public float cardWidth = 140f;
    public Card[] cards;
    public Card cardPrefab; //TEMP

    public int maxHandSize = 10;
    private int handSize = 0;

    // handLength = (cardWidth/2) + (cardWidth/2)*cards.Length;
    // cardInterval = handLength / cards.Length;
    // cardPos = (-handLength/2) + cardInterval*i;
    
    private void Start() {
        cards = new Card[10];
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
            cards[i].transform.localPosition = new Vector3(cardX, 0f, 0f);
        }
    }

    public void Draw(int numCards){
        if(handSize < maxHandSize){
            cards[handSize] = Instantiate(cardPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
            cards[handSize].AdjustTransforms(transform);
            handSize++;
        }
    }
}
