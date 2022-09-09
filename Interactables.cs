using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    [SerializeField] private Animator interactionAnimator;
    [SerializeField] private GameObject player;
    
    [SerializeField] private bool playerCanInteract;

    [Header("Door Parameters")]
    private bool doorOpen = false;
    [SerializeField] Collider doorCollider;

    void Start()
    {
        
    }

    void Update()
    {
        OpenDoor();
    }

    public void HandleDoorOpen()
    {
        if (!doorOpen)
        {
            GetComponent<Animator>().SetBool("DoorIsOpen", true);
            doorOpen = true;
        }

        else if (doorOpen)
        {
            GetComponent<Animator>().SetBool("DoorIsOpen", false);
            doorOpen = false;
        }
    }

    private void OpenDoor()
    {
        if (playerCanInteract && Input.GetKeyDown(KeyCode.E))
        {
            HandleDoorOpen();
        }
    }

    private void OnTriggerEnter(Collider doorCollider)
    {
        var getPlayerInteractionScript = player.GetComponent<InteractionCheck>();

        if(getPlayerInteractionScript == null)
        {
            playerCanInteract = false;
            return;
        }

        else
        {
            playerCanInteract = true;
        }
    }

    private void OnTriggerExit(Collider doorCollider)
    {
        playerCanInteract = false;
    }
}
