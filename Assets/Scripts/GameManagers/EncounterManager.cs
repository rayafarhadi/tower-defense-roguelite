using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterManager : ScriptableObject
{
    public int dealAmount = 5;

    public bool encounterEnded = false;


    public void Init() {
        InitializeDeck();
        PlayerStats.Instance.encounterDeck.Deal(dealAmount);
    }

    public void Update() {

        if(WaveSpawner.Instance.waveStarted){
            PlayerStats.Instance.encounterDeck.hand.DiscardHand();
            WaveSpawner.Instance.waveStarted = false;
        }

        if(WaveSpawner.Instance.waveEnded){

            if (WaveSpawner.Instance.GetCurrentWave() > WaveSpawner.Instance.waves.Length - 1){
                encounterEnded = true;
            }

            PlayerStats.Instance.encounterDeck.Deal(dealAmount);
            WaveSpawner.Instance.waveEnded = false;
            PlayerStats.ResetEnergy();
        }
    }

    private void InitializeDeck(){
        List<Card> playerDeck = PlayerStats.playerDeck;
        PlayerStats.Instance.encounterDeck.deck = new Stack<Card>();

        for (int i = 0; i < playerDeck.Count; i++){
            PlayerStats.Instance.encounterDeck.deck.Push(playerDeck[i]);
        }

        PlayerStats.Instance.encounterDeck.Shuffle();
    }
}
