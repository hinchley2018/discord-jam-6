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
    public Color selectedBackground = Color.yellow;
    public Color selectedForeground = Color.black;
    public Color regularBackground = Color.white;
    public Color regularForeground = Color.grey;
    public Color disabledBackground = Color.grey;
    public Color disabledForeground = Color.grey;
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
        var buttons = new[] {snowmanButton, hutButton, bankButton, factoryButton};
        foreach (var button in buttons)
        {
            button.image.color = button.interactable ? regularBackground : disabledBackground;
            button.transform.GetChild(0).GetComponent<Image>().color = button.interactable ? regularForeground : disabledForeground;
        }

        if (selectedButton)
        {
            selectedButton.image.color = selectedBackground;
            selectedButton.transform.GetChild(0).GetComponent<Image>().color = selectedForeground;
        }
        
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
