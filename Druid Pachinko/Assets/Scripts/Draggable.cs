using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField, Tooltip("the max move speed of this object. Will follow the mouse at this speed when dragged"), Min(0)]private float maxMoveSpeed = 1;
    [SerializeField, Tooltip("if this object can rotate in general")]private bool canSpin = false;
    [SerializeField, Tooltip("if this object can specifically rotate while being dragged. Will only be used if canSpin is set to true")]private bool canSpinWhileDragging = false;
    private bool mouseDrag = false;
    private Rigidbody rb;
    private bool moveable = false;

    private RigidbodyConstraints staticConstraints = RigidbodyConstraints.FreezeAll;
    private RigidbodyConstraints draggingConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetMoveable(moveable);
        if(canSpin){
            staticConstraints &= ~RigidbodyConstraints.FreezeRotationZ;
            if(canSpinWhileDragging){
                draggingConstraints &= ~RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(mouseDrag){
            if(!moveable){
                SetMoveable(true);
            }
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dist = new Vector3(mousePos.x, mousePos.y, transform.position.z) - transform.position;
            Vector3 vel = dist / Time.fixedDeltaTime;
            if(vel.magnitude > maxMoveSpeed){
                vel = vel.normalized * maxMoveSpeed;
            }
            rb.velocity = vel;
            Debug.DrawRay(transform.position, dist, Color.red, 1/50);
            
            //rb.velocity = dist.normalized * (Mathf.Min(dist.magnitude, maxMoveSpeed));
            //Debug.Log("dist: " + dist.magnitude + " from: " + dist + ", max:" + maxMoveSpeed + " for vel: " + rb.velocity);
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), maxMoveSpeed * Time.fixedDeltaTime);
        }
        else{
            if(moveable){
                SetMoveable(false);
            }
            rb.velocity = Vector3.zero;
        }
    }
    private void OnMouseDown() {
        mouseDrag = true;
        Debug.Log("Mouse clicked");
    }
    private void OnMouseUp() {
        mouseDrag = false;
        Debug.Log("Mouse released");
    }

    private void SetMoveable(bool canMove){
        if(canMove){
            rb.constraints = draggingConstraints;

        }
        else{
            rb.constraints = staticConstraints;
        }
        moveable = canMove;
    }
}
