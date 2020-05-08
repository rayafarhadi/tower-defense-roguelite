using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    public Button button;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount(bool isUpgraded){
        int sellAmount = cost/2;
        if (isUpgraded){
            sellAmount += upgradeCost/2;
        }

        return sellAmount;
    }
}
