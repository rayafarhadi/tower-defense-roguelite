using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUIManager : MonoBehaviour
{

    public Text moneyText;

    void Update()
    {
        moneyText.text = "$" + PlayerStats.money.ToString();
    }
}
