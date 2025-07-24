using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject playerBallObject;
    [SerializeField] private Animator animator;

    private bool isDoorOpened;
    private bool isCanUpdateBallPosition;

    public void SetState(bool isCanUpdate)
    {
        isCanUpdateBallPosition = isCanUpdate;
        isDoorOpened = false;

        if (isCanUpdateBallPosition == false)
        {
            animator.Play("Idle");
        }
    }

    private void Update()
    {
        if (isCanUpdateBallPosition == true)
        {
            if (Vector3.Distance(transform.position, playerBallObject.transform.position) < 2.5f && isDoorOpened == false)
            {
                isDoorOpened = true;
                animator.Play("Open");
            }
        }
    }
}
