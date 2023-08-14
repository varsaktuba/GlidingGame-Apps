using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{

    public Animation anim;
    private float startTouch;
    private float swipeDelta;
    float slideSpeed = 0.1f;
    private float swerveAmount = 0.0f;
    Vector3 dir;
    public static bool isStickReleased = false;
    public static float ReleaseForceForRocketman = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

       
        anim["Armature|Bend_Stick"].speed = 0;
        anim["Armature|Bend_Stick"].time = 0;
        anim.Play("Armature|Bend_Stick");
    }




    // Update is called once per frame
    void Update()
    {
        if (!isStickReleased)
        {
            BendStick();
        }
    }

    public void BendStick()
    {
        if (Input.touchCount > 0)
        {


            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                swipeDelta = touch.position.x - startTouch;
                //startTouch = touch.position.x;

                swerveAmount = Time.deltaTime * dir.x;

                dir.x = swipeDelta * slideSpeed;
                if (dir.x < 0)
                    StickAnimationFrameJump(Mathf.Abs(swerveAmount) * anim["Armature|Bend_Stick"].length);

             
            }




            if (touch.phase == TouchPhase.Ended)
            {
                ReleaseForceForRocketman = Mathf.Clamp(Mathf.Abs(swerveAmount), 0.1f, 1.2f);
                swipeDelta = 0;

                anim["Armature|Bend_Stick"].speed = 1;
                anim.Play("Armature|Release_Stick");
                isStickReleased = true;
            }


        }

    }

    public void StickAnimationFrameJump(float time)
    {
        anim["Armature|Bend_Stick"].time = time;
        anim["Armature|Bend_Stick"].speed = 0;
        anim.Play("Armature|Bend_Stick");
    }
}
