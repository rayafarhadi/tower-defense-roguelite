using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUIManager : MonoBehaviour
{

    public GameObject ui;
    public Button upgradeButton;
    public Text upgradeCost;

    public Text sellCost;

    private Node target;

    private void Update() {
        if (target != null){
            if (PlayerStats.money >= target.blueprint.upgradeCost && !target.isUpgraded){
            upgradeButton.interactable = true;
            } else {
                upgradeButton.interactable = false;
            }

            if (upgradeCost.text != "$" + target.blueprint.upgradeCost && !target.isUpgraded){
                upgradeCost.text = "$" + target.blueprint.upgradeCost;
            }

            if (upgradeCost.text != "UPGRADED" && target.isUpgraded){
                upgradeCost.text = "UPGRADED";
            }
        }
    }

    public void SetTarget(Node _target){
        target = _target;

        transform.position = target.GetBuildPosition();

        sellCost.text = "$" + target.blueprint.GetSellAmount(target.isUpgraded);

        ui.SetActive(true);
    }

    public void Hide(){
        target = null;
        ui.SetActive(false);
    }

    public void Upgrade(){
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell(){
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }

}
