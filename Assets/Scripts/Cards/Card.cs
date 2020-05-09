using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IPointerClickHandler
{

    protected BuildManager buildManager;

    public Hand hand;
    public Tower tower;
    public int energyCost;
    public Text energyText;

    [HideInInspector]
    public bool played = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left){
            if(PlayerStats.energy >= energyCost){
                Hand.activeCard = this;
                PerformAction();
            }
        }
    }

    private void Start()
    {
        buildManager = BuildManager.Instance;
        setEnergyText();
        tower.energyCost = energyCost;
    }

    private void Update() {
        if(played){
            hand.Discard(this);
        }
    }

    public abstract void PerformAction();

    private void setEnergyText(){
        energyText.text = energyCost.ToString();
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
