using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractionHandler : NetworkBehaviour
{
    public GameObject interactableObject = null;
    public GameObject interactionHudText;
    public GameObject interactionHandler;

    [SerializeField]
    private Collider interactableObjectCollider = null;

    [SerializeField]
    private Animator animator;

    public string miningAnimName = "Mine";
    public string gatheringAnimName = "Gather";

    private void Start()
    {
        if (!isLocalPlayer)
        {
            enabled = false;
        }
        else
        {
            interactionHandler.SetActive(true);
        }
    }

    void Update()
    {
        if (interactableObjectCollider.IsUnityNull() || !interactableObjectCollider.enabled)
        {
            if (interactionHudText.activeSelf)
                interactionHudText.SetActive(false);
        }
        else
        {
            if (!interactionHudText.activeSelf)
                interactionHudText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Resource resource = interactableObject.GetComponent<Resource>();
                if (resource != null)
                {
                    //TriggerAnim(resource.resourceType);
                    CmdRequestInteractionWithResource(resource);
                }
            }
        }
    }

    public void TriggerAnim(string resourceType)
    {
        switch(resourceType)
        {
            case "Wood":
                animator.SetBool(gatheringAnimName, true);
                break;

            case "Stone":
                animator.SetBool(miningAnimName, true);
                break;

            default:
                break;
        }
    }

    [Command]
    public void CmdRequestInteractionWithResource(Resource resource)
    {
        Debug.Log("Command being sent to server to interact");
        resource.CmdInteractWithResource();
    }

    public void SetInteractionObj(GameObject interactable)
    {
        interactableObject = interactable;
        interactableObjectCollider = null;

        if (!interactableObject.IsUnityNull())
        {
            interactableObjectCollider = interactableObject.GetComponent<Collider>();
        }
    }
}
