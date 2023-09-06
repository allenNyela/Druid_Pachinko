using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPiece : MonoBehaviour
{
    [SerializeField, Tooltip("how quickly this will rotate. positive values will rotate clockwise, negative will rotate counter clockwise")]private float spinSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        transform.RotateAround(transform.position, transform.forward, -spinSpeed);
    }
    
}
