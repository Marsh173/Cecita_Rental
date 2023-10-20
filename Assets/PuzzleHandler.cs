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
    public static bool hasSolvedClockPuzzle = false;
    //private int passwordLength;


    // Start is called before the first frame update
    void Start()
    {
        detectedNames = new List<string>();
        for (int i = 0; i < correctNames.Count; i++) { detectedNames.Add(""); }
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

        bool listFull = true;

        foreach (string inputName in detectedNames)
        {
            if (inputName == "") { listFull = false; break; }
        }

        if (listFull)
        {
            for (int i = 0; i < detectedNames.Count; i++) { detectedNames[i] = ""; }
        }

/*        if (detectedNames.Length > passwordLength)
        {
            System.Array.Clear(detectedNames, 0, correctNames.Length);
        }
*/
    }
}
