using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    private Color defaultColor;

    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TowerBlueprint blueprint;
    [HideInInspector]
    public bool isUpgraded;

    private Renderer r;
    private BuildManager buildManager;

    private void Start()
    {
        r = GetComponent<Renderer>();
        defaultColor = r.material.color;

        buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());

    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            return;
        }

        r.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        r.material.color = defaultColor;
    }

    private void BuildTurret(TowerBlueprint _blueprint)
    {
        blueprint = _blueprint;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        PlayerStats.money -= blueprint.cost;

        BuildManager.Instance.DeselectTurretToBuild();

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {

        //Remove unupgraded turret
        Destroy(turret);

        //Place upgraded turret
        GameObject _turret = (GameObject)Instantiate(blueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //Take money
        PlayerStats.money -= blueprint.upgradeCost;

        //Build effect
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.money += blueprint.GetSellAmount(isUpgraded);
        Destroy(turret);

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        blueprint = null;
        isUpgraded = false;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
