using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainTestScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rainDrop;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(rainDrop, transform.position, transform.rotation);
        }
    }
}
