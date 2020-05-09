using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerCard : Card
{

    public override void PerformAction(){
        buildManager.SelectTurretToBuild(tower);
    }

}
