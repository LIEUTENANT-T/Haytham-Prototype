using UnityEngine;

public class ActivatableDoor : Activatable
{
    private bool isOpen = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        base.Activate();

        toggleOpen();
    }

    public void toggleOpen()
    {
        if (isOpen)
        {
            animator.Play("Close");
            isOpen = false;
        }
        else
        {
            animator.Play("Open");
            isOpen = true;
        }
    }
}
