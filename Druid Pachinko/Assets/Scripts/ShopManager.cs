using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private static ShopManager _instance;
    public static ShopManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShopManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<ShopManager>();
                    Debug.Log("Generating new game manager");
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    private Plant storedPlant = null;

    [SerializeField]
    private Button buySunflowerButton;

    [SerializeField] 
    private Plant sunflowerPrefab;

    [SerializeField]
    private Button buyMushroomButton;

    [SerializeField]
    private Plant mushroomPrefab;

    [SerializeField]
    private Button buyPitcherButton;

    [SerializeField]
    private Plant pitcherPrefab;


    private void FixedUpdate()
    {
        buySunflowerButton.interactable = CheckCanPurchase(sunflowerPrefab);
        buyMushroomButton.interactable = CheckCanPurchase(mushroomPrefab);
        buyPitcherButton.interactable = CheckCanPurchase(pitcherPrefab);
    }

    private bool CheckCanPurchase(Plant aPlant)
    {
        return GameManager.Instance.HasEnoughMoney(aPlant.marketPrice); /**TODO: also check if there are available slots on the board*/
    }

    public void BuyPlant(Plant plant)
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.HasEnoughMoney(plant.marketPrice)) 
        {
            GameManager.Instance.ReduceMoney(plant.marketPrice);
            storedPlant = plant;
        }
    }

    public Plant plantPlant()
    {
        Plant plant2Plant = storedPlant;
        storedPlant = null;
        return plant2Plant;
    }
}
