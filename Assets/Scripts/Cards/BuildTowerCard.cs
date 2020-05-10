using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerCard : Card
{

    public Tower tower;

    public override void Start() {
        base.Start();
        tower.energyCost = energyCost;
    }

    public override void PerformAction(){
        buildManager.SelectTurretToBuild(tower);
    }

}
