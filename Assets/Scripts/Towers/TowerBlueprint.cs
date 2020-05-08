using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TowerBlueprint
{
    public GameObject prefab;
    public int cost;

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
