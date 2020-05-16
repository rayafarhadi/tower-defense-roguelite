using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public static int lives;
    public int startLives = 20;

    public static int energy;
    private static int maxEnergy = 3;
    public Text energyText;

    public static List<Card> playerDeck;

    public Deck encounterDeck;

    private void Awake() {
        if (instance != null)
        {
            Debug.Log("More than one PlayerStats instance");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        playerDeck = StarterDeck();
        energy = maxEnergy;
        lives = startLives;
    }

    private void Update() {
        energyText.text = energy.ToString();
    }

    private List<Card> StarterDeck()
    {
        List<Card> starterDeck = new List<Card>();
        Card[] cards = CardCollection.Instance.GetStarterDeck(CardCollection.StarterDeck.Temp);

        for (int i = 0; i < 10; i++)
        {
            Card card = Instantiate(cards[i], new Vector3(0, 0, 0), Quaternion.identity);
            card.gameObject.SetActive(false);
            starterDeck.Add(card);
        }

        return starterDeck;
    }

    public static void ResetEnergy(){
        energy = maxEnergy;
    }

    public static void AddCardToDeck(Card card){
        card.gameObject.SetActive(false);
        playerDeck.Add(card);
    }

}
