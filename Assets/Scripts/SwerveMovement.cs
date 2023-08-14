using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    SwerveInputSystem swerve;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;


    private Vector3 velocity = Vector3.zero;
    private void Awake()
    {
        swerve = GetComponent<SwerveInputSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * swerve.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);


        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x + swerveAmount, transform.position.y, transform.position.z), ref velocity, 0.1f);

    }
}
