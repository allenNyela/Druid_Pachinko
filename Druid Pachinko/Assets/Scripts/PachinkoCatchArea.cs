using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PachinkoCatchArea : MonoBehaviour
{
    [SerializeField, Tooltip("How many points are earned if a pachinko ball hits this area")] private int points;
    [SerializeField, Tooltip("The tag that the pachinko ball has.")] private string rainDropTag = "RainDrop";
    [SerializeField, Tooltip("The tag that the pachinko ball has.")] private string pollutionDropTag = "PollutionDrop";

    [System.Serializable]public class BallCatchEvent : UnityEvent<int,bool>{};

    [SerializeField, Tooltip("The event that is called when a ball hits this area. Use for any visuals, audio cues, or other interesting things.")]private BallCatchEvent ballCaught;
    
    private void OnTriggerEnter(Collider other) {
        if(!GameManager.Instance.isPlaying()){
            if(other.gameObject.tag == rainDropTag){
                Destroy(other.gameObject);

            }
            if(other.gameObject.tag == pollutionDropTag){
                Destroy(other.gameObject);
            }
            return;
        }
        if(other.gameObject.tag == rainDropTag){
            ballCaught.Invoke(points, true);
            Destroy(other.gameObject);

        }
        if(other.gameObject.tag == pollutionDropTag){
            ballCaught.Invoke(-points, false);
            Destroy(other.gameObject);

        }
    }
}
