using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameEvents _gameEvents;
    [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        _gameEvents.gameOver.AddListener(ShowUI);
    }

    private void OnDisable()
    {
        _gameEvents.gameOver.RemoveListener(ShowUI);
    }

    private void ShowUI()
    {
        panel.SetActive(true);
    }
}
