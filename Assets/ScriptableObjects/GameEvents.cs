using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameEvents : ScriptableObject
{
    public UnityEvent gameOver;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ActivateSelection(GameObject prefab)
    {
        foreach(var buildingPlaceHolder in FindObjectsOfType<BuildingPlaceHolder>())
            buildingPlaceHolder.ActivateSelection(prefab);
    }
    
    public void DeactivateSelection()
    {
        foreach(var buildingPlaceHolder in FindObjectsOfType<BuildingPlaceHolder>())
            buildingPlaceHolder.DeactivateSelection();
    }
}
