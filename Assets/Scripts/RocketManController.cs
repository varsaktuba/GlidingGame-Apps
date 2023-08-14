using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManController : MonoBehaviour
{
    bool oneTimer = false;
    Rigidbody rb;
    [SerializeField]
    private float rotateValue;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (StickController.isStickReleased)
        {
           // RotateRocketman();
            if (!oneTimer)
            {
                oneTimer = true;
                AddForceRocketMan();
            }
        }
    }

    public void AddForceRocketMan()
    {
        gameObject.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(0, StickController.ReleaseForceForRocketman * 5000, StickController.ReleaseForceForRocketman * 5000);

        GameManager.instance.ChangeCam();
    }

    void RotateRocketman()
    {
        // Rotation
        transform.Rotate(rotateValue * Time.deltaTime, 0, 0);
    }
}
