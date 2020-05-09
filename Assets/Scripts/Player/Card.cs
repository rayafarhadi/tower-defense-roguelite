using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{

    private BuildManager buildManager;

    public Hand hand;
    public TowerBlueprint tower;

    public bool played = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left){
            Hand.activeCard = this;
            Build(tower);
        }
    }

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    private void Update() {
        if(played){
            hand.Discard(this);
        }
    }

    public void Build(TowerBlueprint tower)
    {
        buildManager.SelectTurretToBuild(tower);
    }

    public void AdjustTransforms(Transform _transform){
        transform.SetParent(_transform);
        RectTransform rectTransform = (RectTransform) GetComponent(typeof(RectTransform));
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = transform.position;
        rectTransform.pivot = new Vector2(0.5f, 0.5f); 
        rectTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
