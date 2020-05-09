using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerCard : Card
{
  
    public override void PerformAction(){
        buildManager.SelectTurretToBuild(tower);
    }

}
