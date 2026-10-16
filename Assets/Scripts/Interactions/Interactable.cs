using UnityEngine;

public class Interactable : MonoBehaviour, InteractableInterface
{
    public void Interact()
    {
        Debug.Log("Interacting");
    }
}
