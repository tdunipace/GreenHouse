using UnityEngine;

public class PlantBehavior: MonoBehaviour
{
    public Transform playerCam;
    public Animator myAnim;

    private Vector3 targetDir;
    private Vector3 forward;
    void Update()
    {

        CheckLooking();

    }

    void CheckLooking()
    {
        targetDir = playerCam.position - this.transform.position;
        forward = playerCam.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        if (angle < -5.0F)
        {
            //Debug.Log("turn right");
        }
        else if (angle > 5.0F)
        {
            //Debug.Log("turn left");
        }
        else
        {
            //Debug.Log("forward");
        }

            SetAnimations();
    }

    void SetAnimations()
    {
        myAnim.SetFloat("playerDistance", targetDir.magnitude);
        myAnim.SetFloat("playerLook", forward.magnitude);

        //Debug.Log("Distance is " + targetDir.magnitude);
        Debug.Log("Look direction is" + forward.magnitude);
    }
}
