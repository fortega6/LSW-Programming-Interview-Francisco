using UnityEngine;

public class Curtain : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation()
    {
        Debug.Log("PlayAnim");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            animator.Play("Open");
            //animator.SetTrigger("Open");
        }
        else
        {
            animator.Play("Close");
            //animator.SetTrigger("Close");
        }
    }
}
