using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject HealthPrefab;
    public Transform Parent;
    private List<GameObject> Healths = new List<GameObject>();

    void Start()
    {
        RenderHealth();
    }

    public void RenderHealth()
    {
        if (GameManager.gameManager.HealthGame <= 0) return;

        foreach (var item in Healths)
        {
            Destroy(item.gameObject);
            Healths.Remove(item);
        }

        for (int i = 0; i < GameManager.gameManager.Health; i++)
        {
            GameObject Clone = Instantiate(HealthPrefab, Parent);
            Healths.Add(Clone);
        }
    }
}