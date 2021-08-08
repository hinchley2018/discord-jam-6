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
            "Hello welcome to Chill Out And Build!",
            "You play as an aspiring business tycoon, who has come North to the small town of Winteria to start building your empire",
            "You saved up some money so start construction on some buildings, click the buttons below to get started!",
            "Keep in mind you only have so much space in Winteria, so choose carefully",
            
        },
        //1 snowmen attack
        new List<string>
        {
            "<b>Snowmen:</b> We thought the subzero temperatures would be enough to stop you humans from melting our snow.",
            "<b>Snowmen:</b> Your factories are polluting the atmosphere and melting our polar palaces!",
            "<b>Snowmen:</b> If you want something done right you have to do it yourself. Die humans!",
            
        },
        //2 no dialog
        new List<string>(),
        //3 no dialog
        new List<string>(),
        //4 no dialog
        new List<string>(),
        //5 - boss play dialog
        new List<string>
        {
            "<b>Snowman:</b> Chill out! If you don't stop melting us, we will send out our big brother",
            "<b>Snowmen:</b> Alright well we warned you, and you didn't listen",
        },
        //6 no dialog
        new List<string>(),
        //7 no dialog
        new List<string>(),
        //8 - almost done
        new List<string>
        {
            "<b>Snowman:</b> Where are you getting all your building materials?! We cut off your supply line to the airport!!",
            "<b>Snowmen:</b> We have ice cold veins, we will destroy you no matter how many snowmen are lost! Our winter wonderland will be protected at any cost!",
        },
        //6 no dialog
        new List<string>(),
        //10
        new List<string>
        {
            "<b>Snowmen:</b> We brought all our big brothers from the neighboring towns. This is the end of your pollution and heat!"
        }
    };

    private void OnEnable()
    {
        _dialogCanvas.SetActive(false);
        gameEvents.startWave.AddListener(HandleStartWave);
        StartCoroutine(WaitForOthersToOnEnableAndThenStartWave());
    }

    private void OnDisable()
    {
        gameEvents.startWave.RemoveListener(HandleStartWave);
    }

    private IEnumerator WaitForOthersToOnEnableAndThenStartWave()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        gameEvents.startWave.Invoke(0);
    }

    private void HandleStartWave(int waveNum)
    {
        //Debug.Log($"DialogManager: Beginning of {waveNum}");
        //get dialog list for that wave
        waveIndex = waveNum;
        var waveDialogList = dialogStorage[waveIndex];
        if (waveDialogList.Count > 0)
        {
            Time.timeScale = 0.00001f;

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
        if (waveDialogList.Count == innerDialogIndex)
        {
            //hide dialog canvas
            //Debug.Log("hiding dialog");
            _dialogCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }


}
