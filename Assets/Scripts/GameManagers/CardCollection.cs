using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    public Card[] collection;

    public Card[] tempStarterDeck;
    public enum StarterDeck{
        Temp
    }

    private static CardCollection instance;
    public static CardCollection Instance {
        get { return instance; }
    }

    private void Awake() {
        if (instance != null)
        {
            Debug.Log("More than one CardCollection instance");
            return;
        }
        instance = this;
    }

    public Card[] GetStarterDeck(StarterDeck deck){
        if (deck == StarterDeck.Temp){
            return tempStarterDeck;
        }

        return tempStarterDeck;
    }
}
