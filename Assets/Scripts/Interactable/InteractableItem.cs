using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    [HideInInspector] public bool interacted;
    protected private bool interactedAndClicked = false;
    public NormalItems NItem;
    public PlaylistItems AItem;

    public Renderer materialRenderer;
    public Color originalColor;
    public Color blinkColor = Color.black;
    [Range(0, 1)] public float blinkFadeRate = 1;
    public float blinkCoolDownTimer = 100;
    private float blinkCoolDownTimerOriginal;
    public float blinkDuration = 2f;
    private bool hasStartedBlink = false;
    private bool hasEndedBlink = false;
    Coroutine coroutineReference;

    private void Start()
    {
        interacted = false;
        materialRenderer = GetComponent<Renderer>();
        materialRenderer.material.color = originalColor;
        blinkCoolDownTimerOriginal = blinkCoolDownTimer;
    }


    // Update is called once per frame
    void Update()
    {

        //RunBlinkLogic();

        //LMB to open
        if (interacted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickupMe();
                interacted = false;
            }
        }
    }

    protected IEnumerator Blink()
    {
        hasStartedBlink = true;
        hasEndedBlink = false;
        yield return new WaitForSeconds(blinkDuration);
        hasStartedBlink = false;
        hasEndedBlink = true;
    }

    private void RunBlinkLogic()
    {
        blinkCoolDownTimer--;
        if (blinkCoolDownTimer < 0)
        {
            coroutineReference = StartCoroutine(Blink());
            blinkCoolDownTimer = blinkCoolDownTimerOriginal;
        }

        if (hasStartedBlink)
        {
            materialRenderer.material.color = Color.Lerp(materialRenderer.material.color, blinkColor, blinkFadeRate);
        }

        if (hasEndedBlink)
        {
            materialRenderer.material.color = Color.Lerp(materialRenderer.material.color, originalColor, blinkFadeRate);
        }
    }

    private void PickupMe()
    {
        Debug.Log("collected");
        if (!InventoryManager.Instance.NItems.Contains(NItem))
            InventoryManager.Instance.AddNormal(NItem);
        //playlistManager.Instance.Add(NItem);
        Destroy(gameObject);
    }

    public void RecordMe()
    {
        Debug.Log("Recorded");
        if (!RecorderInventoryManager.Instance.AItems.Contains(AItem))
            RecorderInventoryManager.Instance.AddPlaylist(AItem);
        //playlistManager.Instance.Add(NItem);
        //Destroy(gameObject);
    }
    protected override void Interact()
    {
        //Debug.Log("Interacted with Player");
        interacted = true;
    }

    protected override void DisableInteract()
    {
        //Debug.Log("Interacted with Player");
        interacted = false;
    }

}
