using UnityEngine;

public class InteractableButton : BaseInteract
{
    public Activatable linkedActivatable;

    public override void Interact()
    {
        base.Interact();
        linkedActivatable.Activate();
    }
}
