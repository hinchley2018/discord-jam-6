using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameEvents gameEvents;
    [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        panel.SetActive(false);
        gameEvents.gameOver.AddListener(ShowUI);
    }

    private void OnDisable()
    {
        gameEvents.gameOver.RemoveListener(ShowUI);
    }

    private void ShowUI()
    {
        panel.SetActive(true);
    }
}
