using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PachinkoCatchArea : MonoBehaviour
{
    [SerializeField, Tooltip("How many points are earned if a pachinko ball hits this area")] private int points;
    [SerializeField, Tooltip("The tag that the pachinko ball has.")] private string pachinkoBallTag = "RainDrop";

    [System.Serializable]public class BallCatchEvent : UnityEvent<int>{};

    [SerializeField, Tooltip("The event that is called when a ball hits this area. Use for any visuals, audio cues, or other interesting things.")]private BallCatchEvent ballCaught;
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == pachinkoBallTag){
            ballCaught.Invoke(points);
            Destroy(other.gameObject);

        }
    }
}
