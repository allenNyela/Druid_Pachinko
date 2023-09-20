using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("the current score")]private int currentScore = 0;
    [SerializeField, Tooltip("starting money")]private int startingMoney = 5;
    [SerializeField, Tooltip("the current money")]private int currentMoney = 0;
    [SerializeField, Tooltip("the lowest cost for a plant")]private int lowestPlantCost = 5;
    [SerializeField, Tooltip("the text box for the player's score")]private TMP_Text scoreText;
    [SerializeField, Tooltip("the text box for the player's money")]private TMP_Text moneyText;
    [SerializeField, Tooltip("the game over menu")]private GameObject GameOverMenu;
    [SerializeField, Tooltip("the game over menu")]private GameObject GameOverMenu2;
    [SerializeField, Tooltip("the game over score text field")]private TMP_Text gameOverScoreText;
    private bool gameOver = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get{
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<GameManager>();
                    Debug.Log("Generating new game manager");
                }
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameOverMenu.SetActive(false);
        if(GameOverMenu2){
            GameOverMenu2.SetActive(false);
        }
        currentMoney = startingMoney;
        currentScore = 0;
        UpdateMoneyText();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMoney < lowestPlantCost && !CheckForActivePlants()){
            GameOver();
        }
    }

    public bool CheckForActivePlants(){
        return false;
    }

    /// <summary>
    /// returns the player's current score
    /// </summary>
    /// <returns></returns>
    public int GetScore(){
        return currentScore;
    }

    /// <summary>
    /// sets the player's score to the given value (does nothing if less than zero)
    /// </summary>
    /// <param name="score">the score the player will have</param>
    public void UpdateScore(int score){
        if(score < 0){
            return;
        }
        currentScore = score;
        UpdateScoreText();
    }

    /// <summary>
    /// adds the given amount to the player's score
    /// </summary>
    /// <param name="score">the amount to increase the score by</param>
    public void IncreaseScore(int score, bool isPositive = true){
        Debug.Log("added points");
        if(isPositive){
            currentScore += score;
        }
        else{
            currentScore -= score;
        }
        UpdateScoreText();
    }

    /// <summary>
    /// returns the player's current money
    /// </summary>
    /// <returns></returns>
    public int GetMoney(){
        return currentMoney;
    }

    /// <summary>
    /// sets the player's money to the given value (does nothing if given less than zero)
    /// </summary>
    /// <param name="money">the amount of money the player will have</param>
    public void UpdateMoney(int money){
        if(money < 0){
            return;
        }
        currentMoney = money;
        UpdateMoneyText();
    }

    /// <summary>
    /// adds the given amount to the player's money. used when harvesting plants
    /// </summary>
    /// <param name="money">the amount of money to add</param>
    public void AddMoney(int money){
        currentMoney += money;
        UpdateMoneyText();
    }

    /// <summary>
    /// reduces the player's money by the given amount. used when buying stuff from the shop. cannot reduce money below 0
    /// </summary>
    /// <param name="money">the amount of money to subtract from the player</param>
    public void ReduceMoney(int money){
        if(money > currentMoney){
            return;
        }
        currentMoney -= money;
        UpdateMoneyText();
    }

    /// <summary>
    /// Checks if the player has more than a certain amount of money. For use in checking if the player can afford shop items.
    /// </summary>
    /// <param name="moneyVal">the amount of money to compare to the player's money</param>
    /// <returns>true if the player's money is greater than or equal to moneyVal</returns>
    public bool HasEnoughMoney(int moneyVal){
        return currentMoney >= moneyVal;
    }

    private void UpdateScoreText(){
        scoreText.text = currentScore.ToString();
    }
    private void UpdateMoneyText(){
        moneyText.text = currentMoney.ToString();
    }

    private void GameOver(){
        gameOver = true;
        gameOverScoreText.text = "Final Score: " + currentScore;
        GameOverMenu.SetActive(true);
        if(GameOverMenu2){
            GameOverMenu2.SetActive(true);
        }
    }

    public bool isPlaying(){
        return !gameOver;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }
    public void Quit(){
        Application.Quit();
    }
}
