using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public static StickController instance;
    public Animation anim;

    private float startTouch;
    private float swipeDelta;
    private float swerveAmount = 0.0f;
    private float slideSpeed = 0.1f;
    public static float ReleaseForceForRocketman = 0f;
    
    
    Vector3 dir;

    public static bool isStickReleased = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

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
    /// <summary>
    /// Stick release function. Calculate the touch position with swipes then release the stick.
    /// </summary>
    public void BendStick()
    {
        if (Input.touchCount > 0 && !GameManager.gameOver)
        {


            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position.x;
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                swipeDelta = touch.position.x - startTouch;
 

                swerveAmount =  dir.x *0.016f;
    

                dir.x = swipeDelta * slideSpeed;

                if (dir.x < 0)
                    StickAnimationFrameJump(Mathf.Abs(swerveAmount) * anim["Armature|Bend_Stick"].length);
            }




            if (touch.phase == TouchPhase.Ended)

            {
                StickAnimationFrameJump(Mathf.Abs(swerveAmount) * anim["Armature|Bend_Stick"].length);
               
                if (anim["Armature|Bend_Stick"].time > 0.037f)
                {
                    ReleaseForceForRocketman = Mathf.Clamp(Mathf.Abs(swerveAmount), 0.1f, 1.2f);
                    swipeDelta = 0;

                    anim["Armature|Bend_Stick"].speed = 1;
                    anim.Play("Armature|Release_Stick");
                    isStickReleased = true;
                }
            }


        }

    }
    /// <summary>
    /// Stick bending animation plays according to the swipe amount.
    /// </summary>
    /// <param name="time"></param>
    public void StickAnimationFrameJump(float time)
    {
        anim["Armature|Bend_Stick"].time = time;
        anim["Armature|Bend_Stick"].speed = 0;
        anim.Play("Armature|Bend_Stick");
    }
    /// <summary>
    /// Game starts with the default settings.
    /// </summary>
    public void SetGameDefaultSettings()
    {
        isStickReleased = false;
        anim["Armature|Bend_Stick"].speed = 0;
        anim["Armature|Bend_Stick"].time = 0;
        anim.Play("Armature|Bend_Stick");
       
       
    }
}
