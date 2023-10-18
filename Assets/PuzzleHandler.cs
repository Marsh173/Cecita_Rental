using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleHandler : InteractableItem                               //Puzzle Logic partially handled by PlayAudio.cs
{
    public string[] correctNames = { "Correct Object 1", "Correct Object 2", "Correct Object 3", "Correct Object 4" };
    public string[] detectedNames = { "Input 1", "Input 2", "Input 3", "Input 4" };
    public UnityEvent EventOnPuzzleSolved;
    public static bool hasSolvedClockPuzzle = false;
    //private int passwordLength;


    // Start is called before the first frame update
    void Awake()
    {
        //passwordLength =  correctNames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (interacted)
        {
            if (correctNames.SequenceEqual(detectedNames) && !hasSolvedClockPuzzle)
            {
                EventOnPuzzleSolved.Invoke();
                Debug.Log("PuzzleSolved");
                hasSolvedClockPuzzle = true;
            }
        }

        if (detectedNames[0] != "" && detectedNames[1] != "" && detectedNames[2] != "" && detectedNames[3] != "")
        {
            detectedNames[0] = "";
            detectedNames[1] = "";
            detectedNames[2] = "";
            detectedNames[3] = "";
        }

/*        if (detectedNames.Length > passwordLength)
        {
            System.Array.Clear(detectedNames, 0, correctNames.Length);
        }
*/
    }
}
