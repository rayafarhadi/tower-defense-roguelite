using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public Deck encounterDeck;
    public int dealAmount = 5;


    private void Start() {
        InitializeDeck();
        encounterDeck.Deal(dealAmount);
    }

    private void Update() {
        if(waveSpawner.waveEnded){
            encounterDeck.Deal(dealAmount);
            waveSpawner.waveEnded = false;
        }
    }

    private void InitializeDeck(){
        List<Card> playerDeck = PlayerStats.playerDeck;
        encounterDeck.deck = new Stack<Card>();
        for (int i = 0; i < playerDeck.Count; i++){
            encounterDeck.deck.Push(playerDeck[i]);
        }
    }
}
