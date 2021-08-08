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
            "<b>Narration:</b> Hello welcome to Chill Out And Build!",
            "<b>Narration:</b> You play as an aspiring business tycoon, who has come North to the small town of Winteria to start building your empire",
            "<b>Narration:</b> You saved up some money so start construction on some buildings, click the buttons below to get started!",
            "<b>Narration:</b> If you don't have enough money for a factory, buy a hot cocoa hut and earn passive money",
            "<b>Narration:</b> Keep in mind you only have so much space in Winteria, so choose carefully",
            "<b>Narration:</b> As you started building factories in the beautiful little town, you got the occasional environmental complaint letter in the mail.",
            "<b>Narration:</b> Build some snowmen so the townspeople will leave you alone!",
            "<b>Snowmen:</b> We thought our letters would be enough to get you to stop melting our snow.",
            "<b>Snowmen:</b> If you want something done right you have to do it yourself. Die humans!",
            "<b>Narration:</b> Factories produce heat. Hurry and melt them before the destory the town center",
            
        },
        //1 no dialog
        new List<string>(),
        //2 no dialog
        new List<string>(),
        //3 no dialog
        new List<string>(),
        //5 - boss play dialog
        new List<string>
        {
            "<b>Snowman:</b> Chill out dude stop melting us, or we will send out our big brother",
            "<b>Snowmen:</b> Alright well we warned you, and you didn't listen",
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

        //On the zeroth wave we control wave start invocation
        if (waveIndex == 0 && (waveDialogList.Count - 1 == innerDialogIndex) ){
            //hide dialog canvas
            Debug.Log("hiding dialog");
            _dialogCanvas.SetActive(false);

            gameEvents.startWave.Invoke(waveIndex + 1);
        }
    }


}
