using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TowerBlueprint standardTurret;
    public TowerBlueprint missleLauncher;
    public TowerBlueprint laserBeamer;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {

        buildManager.SelectTurretToBuild(standardTurret);

    }

    public void SelectMissleLauncher()
    {

        buildManager.SelectTurretToBuild(missleLauncher);

    }

    public void SelectLaserBeamer()
    {

        buildManager.SelectTurretToBuild(laserBeamer);

    }

}
