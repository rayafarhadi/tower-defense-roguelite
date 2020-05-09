﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int lives;
    public int startLives = 20;

    public static int energy;
    private static int maxEnergy = 3;

    public static List<Card> playerDeck;

    public Card cardPrefab; //TEMP

    private void Awake()
    {
        playerDeck = StarterDeck();
    }

    private void Start()
    {
        energy = maxEnergy;
        lives = startLives;
    }

    private List<Card> StarterDeck()
    {
        List<Card> starterDeck = new List<Card>();

        for (int i = 0; i < 10; i++)
        {
            Card card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            card.gameObject.SetActive(false);
            starterDeck.Add(card);
        }

        return starterDeck;
    }

    public static void ResetEnergy(){
        energy = maxEnergy;
    }

}
