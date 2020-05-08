using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIManager : MonoBehaviour
{
    public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStats.lives + " LIVES";
    }
}
