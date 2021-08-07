using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowmanPlaceHolder : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer spriteRenderer;
    public Color inactiveColor = Color.clear;
    public Color activeColor = Color.green;
    public bool selectionActive;
    public GameEvents gameEvents;

    private void OnEnable()
    {
        spriteRenderer.color = inactiveColor;
    }

    public void ActivateSelection()
    {
        spriteRenderer.color = activeColor;
        selectionActive = true;
    }

    public void DeactivateSelection()
    {
        spriteRenderer.color = inactiveColor;
        selectionActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MakeSelection();
    }

    private void MakeSelection()
    {
        if (!selectionActive) return;
        if (gameEvents && gameEvents.buildingPrefab)
        {
            var building = Instantiate(gameEvents.buildingPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
        }
}
