using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerMovement : MonoBehaviour
{
    
    private PhotonView PV;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    public Rigidbody2D camRigidbody;
    public GameObject player;
    Vector3 CameraPos;
    Vector3 PlayerPos;

    void Start()
    {	
       PV = GetComponent<PhotonView>();
       PlayerPos = this.transform.position;
       CameraPos = cam.transform.position;
       this.transform.rotation = Quaternion.Euler(0, 0, 90);


        if(!PV.IsMine)
        {
          Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void Update()
    {
        if(!PV.IsMine)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        print(mousePos);
    }

    void FixedUpdate()
    {
    	if(!PV.IsMine)
    	{
    		return;
    	}

        Move();
        
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        camRigidbody.MovePosition(rb.position);
        camRigidbody.rotation = 0;

        cam.transform.rotation = Quaternion.Euler(0, 0, 0);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
