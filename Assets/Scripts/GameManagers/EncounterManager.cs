using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public Deck encounterDeck;
    public int dealAmount = 5;

    public Text energyText;


    private void Start() {
        InitializeDeck();
        encounterDeck.Deal(dealAmount);
    }

    private void Update() {

        if(waveSpawner.waveStarted){
            encounterDeck.hand.DiscardHand();
            waveSpawner.waveStarted = false;
        }

        if(waveSpawner.waveEnded){
            encounterDeck.Deal(dealAmount);
            waveSpawner.waveEnded = false;
            PlayerStats.ResetEnergy();
        }

        energyText.text = PlayerStats.energy.ToString();

    }

    private void InitializeDeck(){
        List<Card> playerDeck = PlayerStats.playerDeck;
        encounterDeck.deck = new Stack<Card>();

        for (int i = 0; i < playerDeck.Count; i++){
            encounterDeck.deck.Push(playerDeck[i]);
        }

        encounterDeck.Shuffle();
    }
}
