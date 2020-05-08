using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{
    BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.Instance;
    }

    public void Build(TowerBlueprint tower){
        buildManager.SelectTurretToBuild(tower);
    }
}
