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

    private TowerBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUIManager nodeUI;
    public bool CanBuild
    {
        get { return turretToBuild != null; }
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

    public void SelectTurretToBuild(TowerBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public void DeselectTurretToBuild(){
        turretToBuild = null;
    }

    public TowerBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Node node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

}
