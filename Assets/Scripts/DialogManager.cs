using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameplayCanvas;
    [SerializeField] private GameObject _dialogCanvas;
    [SerializeField] private TextMeshProUGUI _dialogTextWidget;
    private int waveIndex;
    private int innerDialogIndex;
    public GameEvents gameEvents;
    private List<List<string>> dialogStorage = new List<List<string>>
    {
        //0
        new List<string>
        {
            "<b>Snowman Says:</b> Hello welcome to Chill Out and Build",
            "Second dialog text",
            "Third dialog text"
        },
        //1 no dialog
        new List<string>(),
        new List<string>
        {
            "<b>Snowman Says:</b> Wave 1 incoming!",
            "<b>Snowman Says:</b> Chill out dude stop melting us",
        }
    };

    private void OnEnable()
    {
        gameEvents.startWave.AddListener(HandleStartWave);
        gameEvents.startWave.Invoke(0);
    }

    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(HandleStartWave);
    }

    private void HandleStartWave(int waveNum)
    {
        Debug.Log($"DialogManager: Beginning of {waveNum}");
        //get dialog list for that wave
        waveIndex = waveNum;
        var waveDialogList = dialogStorage[waveIndex];
        if (waveDialogList.Count > 0)
        {
            //reset dialog index
            innerDialogIndex = 0;

            //set gameplay canvas inactive
            //_gameplayCanvas.SetActive(false);

            _dialogCanvas.SetActive(true);

            //set dialog text
            _dialogTextWidget.text = waveDialogList[innerDialogIndex];
            
        }

    }
    

    //next dialog
    public void NextDialog()
    {
        //get dialog from storage
        var waveDialogList = dialogStorage[waveIndex];
        innerDialogIndex += 1;
        if (waveDialogList.Count > innerDialogIndex)
        {
            //get next text
            var nextText = waveDialogList[innerDialogIndex];
            _dialogTextWidget.text = nextText;
        }

        //now are we done with dialog? (3 elements) index: 2
        if (waveDialogList.Count - 1 == innerDialogIndex ){
            //hide dialog canvas
            Debug.Log("hiding dialog");
            _dialogCanvas.SetActive(false);

            gameEvents.startWave.Invoke(waveIndex + 1);
        }
    }


}
