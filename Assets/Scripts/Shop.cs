using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public TurretBlueprint standardTurret;
    public TurretBlueprint missleLauncher;
    public TurretBlueprint laserBeamer;

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

    private void Update()
    {
        if (PlayerStats.money >= standardTurret.cost)
        {
            standardTurret.button.interactable = true;
        }
        else
        {
            standardTurret.button.interactable = false;
        }

        if (PlayerStats.money >= missleLauncher.cost)
        {
            missleLauncher.button.interactable = true;
        }
        else
        {
            missleLauncher.button.interactable = false;
        }

        if (PlayerStats.money >= laserBeamer.cost)
        {
            laserBeamer.button.interactable = true;
        }
        else
        {
            laserBeamer.button.interactable = false;
        }

    }
}
