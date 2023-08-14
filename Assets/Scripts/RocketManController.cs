using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketManController : MonoBehaviour
{
    public static RocketManController instance;
    public Animation anim;

    bool oneTimer = false;
    bool isRocketmanWing = false;
    bool stopRotating = false;

    Rigidbody rb;
    [SerializeField]
    private float rotateValue;
    public float forceSpeed;
    public float rotateSpeed;
    private float startTouch;
    private float swipeDelta;
    public static float swerveAmount = 0.0f;
    float slideSpeed = 5f;

    public int multiplier;
    Vector3 dir;

    Vector3 gameObjOrjPos;

    public GameObject RocketManParent;
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
        
       
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        gameObjOrjPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (StickController.isStickReleased && !GameManager.gameOver)
        {
          
            if (!oneTimer)
            {
                oneTimer = true;
                AddForceRocketMan();
            }
            if (!stopRotating)
            {
                RotateRocketman();
            }
            
        }

        if (isRocketmanWing)
        {

            Wing();
        }
    }
    /// <summary>
    /// Rocketman wing animation control according to the touch states.
    /// </summary>
    public void Wing()
    {
        if (Input.touchCount > 0 && !GameManager.gameOver)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                stopRotating = true;
                startTouch = touch.position.x;
                transform.DORotate(new Vector3(50, 0, 0), 0.2f);
                anim.Play("Armature|1_Open_wings_2");
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                gameObject.transform.GetChild(3).gameObject.SetActive(true);

            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                swipeDelta = touch.position.x - startTouch;
                startTouch = touch.position.x;
                transform.Translate(swerveAmount, 0, 0);
                rb.velocity = new Vector3(0, 0, rb.velocity.z);
                var rotateAmount = Mathf.Clamp(-swerveAmount * 10, -10, 10);
                transform.DORotate(new Vector3(50, 0, rotateAmount), 0.1f);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                stopRotating = false;
                swipeDelta = 0;
                //Physics.gravity = new Vector3(0, -9.81f, 0);
                anim.Play("Armature|2_Close_wings");
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                gameObject.transform.GetChild(3).gameObject.SetActive(false);
            }

            dir.x = swipeDelta * slideSpeed;
            swerveAmount = Time.deltaTime * dir.x;
        }
    }
    /// <summary>
    /// When release the stick adding force the rocketman.
    /// </summary>
    public void AddForceRocketMan()
    {
        gameObject.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(0, StickController.ReleaseForceForRocketman * forceSpeed, StickController.ReleaseForceForRocketman * forceSpeed,ForceMode.Force);
        GameManager.instance.ChangeCam();
        StartCoroutine(waitAndMakeWingsTrue());
    }
    IEnumerator waitAndMakeWingsTrue()
    {
        yield return new WaitForSeconds(0.5f);
        isRocketmanWing = true;
    }
    /// <summary>
    /// Rotating rocketman if wings are closed.
    /// </summary>
    void RotateRocketman()
    {
        // Rotation
        transform.Rotate(rotateValue * Time.deltaTime, 0, 0);
    }

 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("X"))
        {
            JumpupTheRocketman(multiplier);
        }
        if (collision.gameObject.CompareTag("2X"))
        {
            JumpupTheRocketman(multiplier * 2);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.gameOver = true;
            GameManager.instance.GameOverPanel.SetActive(true);
            
        }
    }
    /// <summary>
    /// If rocketman can fall the obstacles, that obstacles bouncing the rocketman.
    /// </summary>
    /// <param name="mult"></param>
    public void JumpupTheRocketman(int mult)
    {
        rb.AddForce(0, multiplier * forceSpeed, multiplier * forceSpeed);
    }


    /// <summary>
    /// Game starts with the default settings.
    /// </summary>
    public void SetGameDefaultSettings()
    {
        gameObject.transform.position = gameObjOrjPos;
        gameObject.transform.rotation = Quaternion.identity;
        StickController.isStickReleased = false;
        oneTimer = false;
        isRocketmanWing = false;
        stopRotating = false;
        gameObject.transform.SetParent(RocketManParent.transform);
        rb.isKinematic = true;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);

        anim.Play("Armature|2_Close_wings");

    }

   
}
