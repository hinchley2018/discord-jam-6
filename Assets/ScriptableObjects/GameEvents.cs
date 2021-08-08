using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameEvents : ScriptableObject
{
    public UnityEvent<int> startWave;
    public UnityEvent gameOver;
    public UnityEvent finishSpawning;
    public UnityEvent allWaveSnowmenMelted;
    public GameObject buildingPrefab;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    public void ActivateSelection(GameObject prefab)
    {
        DeactivateSelection();
        buildingPrefab = prefab;
        foreach(var buildingPlaceHolder in FindObjectsOfType<BuildingPlaceHolder>())
            buildingPlaceHolder.ActivateSelection();
    }
    
    public void ActivateSnowmanSelection(GameObject prefab)
    {
        DeactivateSelection();
        buildingPrefab = prefab;
        foreach(var snowmanPlaceHolder in FindObjectsOfType<SnowmanPlaceHolder>())
            snowmanPlaceHolder.ActivateSelection();
    }
    
    public void DeactivateSelection()
    {
        buildingPrefab = null;
        foreach(var snowmanPlaceHolder in FindObjectsOfType<SnowmanPlaceHolder>())
            snowmanPlaceHolder.DeactivateSelection();
        foreach(var buildingPlaceHolder in FindObjectsOfType<BuildingPlaceHolder>())
            buildingPlaceHolder.DeactivateSelection();
    }
}
