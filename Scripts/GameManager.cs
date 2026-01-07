using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int Score;
    public int Money;
    public float CoinSpeed = 0.1f;
    public float BombSpeed = 5f;
    public float MagnitSpeed = 15;
    public int MaxCoins = 10;
    public int Total_coins = 0;
    public int Total_magnits = 0;
    public int Total_score = 0;
    public int MoneyUpgradeLevel = 0;
    public int HealthUpgradeLevel = 0;
    public int Health = 0;
    public int HealthGame = 0;
    public int MaxMoney = 0;

    public void Awake()
    {
        gameManager = this;
        DontDestroyOnLoad(gameManager);
    }

    public void AddMoney(int pupu)
    {
        Money += pupu;
    }
}
