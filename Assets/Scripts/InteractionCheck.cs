using Unity.VisualScripting;
using UnityEngine;

public class InteractionCheck : MonoBehaviour
{
    public PlayerInteractionHandler interactionHandler;

    private Collider currentCollider = null;

    public void FixedUpdate()
    {
        if(!currentCollider.IsUnityNull())
        {
            if (!currentCollider.enabled)
            {
                currentCollider = null;
                interactionHandler.SetInteractionObj(null);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentCollider = other;
        interactionHandler.SetInteractionObj(currentCollider.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(currentCollider == other)
        {
            interactionHandler.SetInteractionObj(null);
        }
    }
}
