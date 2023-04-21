using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Tracer : MonoBehaviour
{
    public LayerMask exceptLayer;
    public Transform target;
    public float forwardDistance;
    
    public float yDistance;
    public float smoothDamp;
    public float rotateSpeed;
    private float sightVertical;
    public float xRot;

    public float first_forwardDistance;

    private Vector3 blockCheckPosition;

    void Awake()
    {
        first_forwardDistance = forwardDistance;    
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position + Vector3.up * yDistance - (target.forward * forwardDistance), Time.deltaTime * smoothDamp);

        // Mouse Y축 회전
        sightVertical = Input.GetAxis("Mouse Y") * (-1);
        xRot += (Vector3.right * sightVertical * rotateSpeed * 100 * Time.deltaTime).x;
        xRot = Mathf.Clamp(xRot, -20, 50);
        target.eulerAngles = new Vector3(xRot, target.eulerAngles.y, target.eulerAngles.z);

        // 벽뚫기 방지
        CheckTarget();

        // 3인칭 시점        
        transform.LookAt(target.position);

        // 정조준
        target.localEulerAngles = new Vector3(xRot, 0, 0);
        
    }

    void CheckTarget()
    {
        blockCheckPosition = (target.position + Vector3.up * yDistance - (target.forward * first_forwardDistance));
                
        RaycastHit hit;
        if (Physics.Raycast(blockCheckPosition, target.parent.position - blockCheckPosition, out hit, 20, exceptLayer))
        {
            if (hit.collider.gameObject == target.parent.gameObject)
            {
                forwardDistance = first_forwardDistance;
            }
            else
            {
                if (Physics.Raycast(transform.position, target.parent.position - transform.position, out hit, 20, exceptLayer))
                {
                    if (hit.collider.gameObject != target.parent.gameObject)
                    {
                        forwardDistance -= 20f * Time.deltaTime;
                    }
                }
            }
            forwardDistance = Mathf.Clamp(forwardDistance, 0.5f, first_forwardDistance);
        }
    }
}
