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
    public Tower tower;
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

        if (tower != null)
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

        if (tower != null)
        {
            return;
        }

        r.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        r.material.color = defaultColor;
    }

    private void BuildTurret(Tower _tower)
    {
        tower = (Tower)Instantiate(_tower, GetBuildPosition(), Quaternion.identity);
        Hand.activeCard.played = true;

        BuildManager.Instance.DeselectTurretToBuild();
        PlayerStats.energy -= tower.energyCost;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
