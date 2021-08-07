using UnityEngine;
using UnityEngine.EventSystems;

public class Background : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameEvents gameEvents;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        gameEvents.DeactivateSelection();
    }
}
