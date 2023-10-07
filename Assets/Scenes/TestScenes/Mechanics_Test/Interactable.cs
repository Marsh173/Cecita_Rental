using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    public void BaseDisableInteract()
    {
        DisableInteract();
    }

    protected virtual void Interact()
    {
        //template function to be overridden by subclasses
    }

    protected virtual void DisableInteract()
    {
        //template function to be overridden by subclasses
    }

}
