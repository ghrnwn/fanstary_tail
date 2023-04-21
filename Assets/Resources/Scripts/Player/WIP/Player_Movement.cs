using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody mybody;

    // Directional Value
    public float horizontal;
    public float vertical;
    public float sightHorizontal;
    public bool stop = false;

    [Header("Movement_Value")]
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public float rotateSpeed;

    void Awake()
    {
        mybody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!stop)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }
        sightHorizontal = Input.GetAxis("Mouse X");

        MoveSpeedController();
        Move();
    }

    void MoveSpeedController()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    void Move()
    {
        transform.Translate(Vector3.Normalize(horizontal * transform.right + vertical * transform.forward) * currentSpeed * Time.deltaTime,Space.World);
        transform.Rotate(Vector3.up * sightHorizontal * rotateSpeed * 100 * Time.deltaTime);
    }
}
