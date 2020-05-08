using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{

    private BuildManager buildManager;

    public Hand hand;
    public TowerBlueprint tower;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Build(tower);
        else if (eventData.button == PointerEventData.InputButton.Middle)
            Debug.Log("Middle click");
        else if (eventData.button == PointerEventData.InputButton.Right)
            Debug.Log("Right click");
    }

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void Build(TowerBlueprint tower)
    {
        buildManager.SelectTurretToBuild(tower);
    }

    public void AdjustTransforms(Transform handTransform){
        transform.SetParent(handTransform);
        RectTransform rectTransform = (RectTransform) GetComponent(typeof(RectTransform));
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = transform.position;
        rectTransform.pivot = new Vector2(0.5f, 0.5f); 
        rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
