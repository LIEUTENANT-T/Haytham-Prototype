using UnityEngine;

public class Activatable : MonoBehaviour
{
    private bool isActivated;

    public virtual void Activate()
    {
        isActivated = true;
    }
}
