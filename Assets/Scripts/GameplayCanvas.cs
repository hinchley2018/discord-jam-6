using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    public GameEvents gameEvents;
    public Button snowmanButton;
    public Button hutButton;
    public Button bankButton;
    public Button factoryButton;
    public Button selectedButton;
    public Sprite regularSnowmanSprite;
    public Sprite regularHutSprite;
    public Sprite regularBankSprite;
    public Sprite regularFactorySprite;
    public Sprite selectedSnowmanSprite;
    public Sprite selectedHutSprite;
    public Sprite selectedBankSprite;
    public Sprite selectedFactorySprite;
    public int hutCost = 1;
    public int bankCost = 20;
    public int factoryCost = 50;
    public CurrencyManager currencyManager;
    public GameObject tooltip;
    [Multiline] public string snowmanTooltip = "Snowman";
    [Multiline] public string hutTooltip = "Hut";
    [Multiline] public string bankTooltip = "Snowman";
    [Multiline] public string factoryTooltip = "Snowman";

    private void Awake()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        tooltip.SetActive(false);
    }

    private void Update()
    {
        snowmanButton.GetComponent<Image>().sprite = regularSnowmanSprite; 
        hutButton.GetComponent<Image>().sprite = regularHutSprite; 
        bankButton.GetComponent<Image>().sprite = regularBankSprite; 
        factoryButton.GetComponent<Image>().sprite = regularFactorySprite; 

        if (selectedButton == snowmanButton) snowmanButton.GetComponent<Image>().sprite = selectedSnowmanSprite;
        if (selectedButton == hutButton) hutButton.GetComponent<Image>().sprite = selectedHutSprite;
        if (selectedButton == bankButton) bankButton.GetComponent<Image>().sprite = selectedBankSprite;
        if (selectedButton == factoryButton) factoryButton.GetComponent<Image>().sprite = selectedFactorySprite;

        hutButton.interactable = currencyManager.money >= hutCost;
        bankButton.interactable = currencyManager.money >= bankCost;
        factoryButton.interactable = currencyManager.money >= factoryCost;
    }

    public void Select(Button button)
    {
        if (selectedButton != button)
            selectedButton = button;
        else
        {
            gameEvents.DeactivateSelection();
            selectedButton = null;
        }
    }

    public void OnPointerEnter(BaseEventData eventData)
    {
        var pointerEventData = (PointerEventData) eventData;
        var button = pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<Button>();
        if (button == snowmanButton)
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = snowmanTooltip;
        if (button == hutButton)
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = hutTooltip;
        if (button == bankButton)
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = bankTooltip;
        if (button == factoryButton)
            tooltip.GetComponentInChildren<TextMeshProUGUI>().text = factoryTooltip;
        tooltip.SetActive(true);
    }
    
    public void OnPointerExit(BaseEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
