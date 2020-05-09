using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    private static BuildManager instance;
    public static BuildManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private Tower towerToBuild;
    private Node selectedNode;
    public bool CanBuild
    {
        get { return towerToBuild != null; }
    }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.Log("More than one BuildManager instance");
            return;
        }
        instance = this;
    }

    public void SelectTurretToBuild(Tower tower)
    {
        towerToBuild = tower;

        DeselectNode();
    }

    public void DeselectTurretToBuild()
    {
        towerToBuild = null;
    }

    public Tower GetTurretToBuild()
    {
        return towerToBuild;
    }

    public void SelectNode(Node node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null;
    }

    public void DeselectNode()
    {
        selectedNode = null;
    }

}
