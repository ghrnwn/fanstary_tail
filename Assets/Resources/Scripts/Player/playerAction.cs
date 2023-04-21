using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAction : MonoBehaviour
{
    // Start is called before the first frame update 
    public float speed;
    Rigidbody2D rigid2D;
    public GameManager manager;
    float h;
    float v;
    Vector3 dirVec;
    GameObject scanObject;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");


        //3d라 방향 다른듯
        if (h < 0)
            dirVec = Vector3.left;
        else if (h > 0)
            dirVec = Vector3.right;

        if (v < 0)
            dirVec = Vector3.down;
        else if (v > 0)
            dirVec = Vector3.up;



        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            //Debug.Log(scanObject.name);
            manager.Action(scanObject);
        }

    }
    private void FixedUpdate()
    {

        rigid2D.velocity = new Vector2(h, v) * speed;

        Debug.DrawRay(rigid2D.position, dirVec * 0.7f, new Color(255, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec * 0.7f,0.7f,LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;


    }
}
