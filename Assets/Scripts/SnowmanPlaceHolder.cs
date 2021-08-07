using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowmanPlaceHolder : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer spriteRenderer;
    public Color inactiveColor = Color.clear;
    public Color activeColor = Color.green;
    public bool selectionActive;
    public GameObject buildingPrefab;

    private void OnEnable()
    {
        spriteRenderer.color = inactiveColor;
    }

    public void ActivateSelection(GameObject prefab)
    {
        spriteRenderer.color = activeColor;
        selectionActive = true;
        buildingPrefab = prefab;
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
        if (buildingPrefab)
        {
            var building = Instantiate(buildingPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
        }
}
