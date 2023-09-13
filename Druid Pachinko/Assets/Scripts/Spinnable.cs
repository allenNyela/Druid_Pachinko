using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinnable : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum rotate speed of this object")]private float maxRotateSpeed = 30;

    private RigidbodyConstraints staticConstraints = RigidbodyConstraints.FreezeAll;
    private RigidbodyConstraints spinningConstraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezeRotationZ;
    private bool spinning = false;
    private bool mouseDrag = false;
    private Rigidbody rb;
    private Vector3 startingMousePos = Vector3.zero;
    private Vector3 previousMousePos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetSpinning(false, Vector3.zero);
    }

    // Update is called once per frame
    private void OnMouseDown() {
        mouseDrag = true;
        Debug.Log("Mouse clicked");
    }
    private void OnMouseUp() {
        mouseDrag = false;
        Debug.Log("Mouse released");
    }

    void FixedUpdate()
    {
        if(mouseDrag){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDir = new Vector3(mousePos.x, mousePos.y, 0) - transform.position;;
            if(!spinning){
                SetSpinning(true, mouseDir);
                return;
            }
            
            Debug.DrawRay(transform.position, mouseDir, Color.red, 1/50);
            Debug.DrawRay(transform.position, previousMousePos, Color.green, 1/50);
            // Vector3 dist = new Vector3(mousePos.x, mousePos.y, transform.position.z) - transform.position;
            // Vector3 vel = dist / Time.fixedDeltaTime;
            float changeAngle = Vector3.Angle(mouseDir, previousMousePos);
            float changeDir = 1;
            Vector3 cross = Vector3.Cross(mouseDir, previousMousePos);
            if(cross.z > 0){
                changeDir = -1;
            }
            changeAngle *= changeDir;
            
            Vector3 angVel = transform.forward * changeAngle;
            if(angVel.magnitude > maxRotateSpeed){
                angVel = angVel.normalized * maxRotateSpeed;
            }
            rb.angularVelocity = angVel;

            previousMousePos = mouseDir;
        }
        else{
            if(spinning){
                SetSpinning(false, Vector3.zero);
            }
            rb.velocity = Vector3.zero;
        }
    }
    
    private void SetSpinning(bool isSpinning, Vector3 mousePos){
        if(isSpinning){
            rb.constraints = spinningConstraints;

        }
        else{
            rb.constraints = staticConstraints;
            
        }
        spinning = isSpinning;
        previousMousePos = mousePos;
        startingMousePos = mousePos;
    }
}
