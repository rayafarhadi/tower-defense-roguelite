using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    protected BuildManager buildManager;

    public Hand hand;
    [HideInInspector]
    public int handPosition;

    public string cardName;
    public Text nameText;

    public int energyCost;
    public Text energyText;

    public Image playableIndicator;
    public Image activeIndicator;
    [HideInInspector]
    public bool played = false;
    public bool reward = true;

    public virtual void Start()
    {
        buildManager = BuildManager.Instance;
        SetEnergyText();
        nameText.text = cardName;
    }

    private void Update()
    {
        if (played)
        {
            hand.Discard(this);
        }

        if (PlayerStats.energy >= energyCost)
        {
            playableIndicator.enabled = true;
        }
        else
        {
            playableIndicator.enabled = false;
        }
    }

    public abstract void PerformAction();

    public void RewardAction(){
        PlayerStats.AddCardToDeck(this);
        Rewards.cardSelected = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (PlayerStats.energy >= energyCost && !reward)
            {
                if(Hand.activeCard != null){
                    Hand.activeCard.activeIndicator.enabled = false;
                }
                Hand.activeCard = this;
                activeIndicator.enabled = true;
                PerformAction();
            } else if (reward){
                RewardAction();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        hand.focusedCardIndex = handPosition;
        hand.OrganizeHand();
        transform.localPosition = new Vector3(transform.localPosition.x, 19f, transform.localPosition.z);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        transform.SetSiblingIndex(handPosition);
        hand.focusedCardIndex = -1;
        hand.OrganizeHand();
    }

    private void SetEnergyText()
    {
        energyText.text = energyCost.ToString();
    }

    public void AdjustTransforms(Transform _transform)
    {
        transform.SetParent(_transform);
        RectTransform rectTransform = (RectTransform)GetComponent(typeof(RectTransform));
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = transform.position;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
