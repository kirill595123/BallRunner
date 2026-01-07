using UnityEngine;
using System.Collections.Generic;

public class Magnit : MonoBehaviour
{
    public items items;
    public GameObject Player;
    public float speed = 10f;
    
    private List<GameObject> collecting = new List<GameObject>();
    
    public void Colect()
    {
        collecting = new List<GameObject>(items.CoinsSpawned);
    }
    
    void Update()
    {
        foreach (var coin in collecting)
        {
            if (coin != null && Player != null)
            {
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, Player.transform.position, speed * Time.deltaTime);
            }
        }
    }
}