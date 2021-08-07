using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    
    public GameEvents gameEvents;
    private List<List<string>> dialogStorage;

    private void OnEnable()
    {
        gameEvents.startWave.AddListener(HandleStartWave);

        dialogStorage = new List<List<string>>
        {
            //0
            new List<string>
            {
                "Hello welcome to Chill Out and Build",
                "Next implement the dialog text"
            }
        };
    }

    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(HandleStartWave);
    }

    private void HandleStartWave(int waveNum)
    {
        //get dialog list for that wave
        var waveDialogList = dialogStorage[waveNum];
        if (waveDialogList.Count > 0)
        {
            for (int i = 0; i < waveDialogList.Count; i++)
            {
                Debug.Log($"DialogManager: {waveDialogList[i]}");
            }
        }
        
    }
}
