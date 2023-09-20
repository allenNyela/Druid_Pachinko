using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HideDisplayButton : MonoBehaviour
{
    [SerializeField, Tooltip("the amount of money needed to buy this one")]private int buttonCost = 5;
    [SerializeField, Tooltip("the button")]private Button button;
    [SerializeField, Tooltip("the image")]private Image image;
    [SerializeField, Tooltip("pressable color")]private Color pressable;
    [SerializeField, Tooltip("pressable color")]private Color unPressable;

    private void Update(){
        bool enabled = GameManager.Instance.HasEnoughMoney(buttonCost);
        button.enabled = enabled;
        if(enabled){
            image.color = pressable;
        }
        else{
            image.color = unPressable;
        }
    }
}
