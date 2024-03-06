using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleHandler : InteractableItem                               //Puzzle Logic partially handled by PlayAudio.cs
{
    [Tooltip("Enter the correct answer <in string> in the correct order here")] public List<string> correctNames;
    public List<string> detectedNames;
    public UnityEvent EventOnPuzzleSolved;
    public UnityEvent EventOnPuzzleFailed;
    [Tooltip("Triggers EventOnPuzzleFailed_End when timer reaches 0")]public int puzzleFailedResetTimer;
    public UnityEvent EventOnPuzzleFailed_End;
    public static bool hasSolvedClockPuzzle = false;
    public static bool correctInput = false;
    //private int passwordLength;
    Coroutine coroutineRef;


    // Start is called before the first frame update
    void Start()
    {
        detectedNames = new List<string>();
        for (int i = 0; i < correctNames.Count; i++) { detectedNames.Add(""); }
    }

    // Update is called once per frame
    void Update()
    {
        if (ableToInteract)
        {
            if (correctNames.SequenceEqual(detectedNames) && !hasSolvedClockPuzzle)
            {
                EventOnPuzzleSolved.Invoke();
                correctInput = true;
                Debug.Log("PuzzleSolved");
                hasSolvedClockPuzzle = true;
            }
        }

        bool listFull = true;

        foreach (string inputName in detectedNames)
        {
            if (inputName == "") { listFull = false; break; }
        }

        if (listFull)
        {
            for (int i = 0; i < detectedNames.Count; i++) { detectedNames[i] = ""; }
            if (!correctInput) { coroutineRef = StartCoroutine(RunInteractEvents());}
        }

/*        if (detectedNames.Length > passwordLength)
        {
            System.Array.Clear(detectedNames, 0, correctNames.Length);
        }
*/
    }

    private IEnumerator RunInteractEvents()
    {
        EventOnPuzzleFailed.Invoke();
        yield return new WaitForSeconds(puzzleFailedResetTimer);                                                       //Put this line on top or middle of this section of code in case we want a hold input to complete the recording
        EventOnPuzzleFailed_End.Invoke();
    }
}
