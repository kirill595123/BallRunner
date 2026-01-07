using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;

    private float Speed;
    public items items;

    void Start()
    {
        Speed = GameManager.gameManager.CoinSpeed;
    }

    void Update()
    {
        Speed -= Time.deltaTime;

        if (Speed <= 0)
        {
            Vector3 RandomPos = new Vector3(UnityEngine.Random.Range(-6, 6), 0.4f, UnityEngine.Random.Range(-6,6));

            var Clone = Instantiate(CoinPrefab, RandomPos, Quaternion.identity);
            Vector3 scale = new Vector3(Clone.transform.localScale.x, Clone.transform.localScale.y, Clone.transform.localScale.z);
            Clone.transform.localScale = new Vector3(0, 0, 0);
            Clone.transform.DOScale(scale, 0.6f).SetEase(Ease.InSine);

            Collider[] colliders = Physics.OverlapSphere(RandomPos, 0.5f);
            bool IsInFire = false;
            
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("DeleteCoinZone"))
                {
                    IsInFire = true;
                    break;
                }
            }
            
            if (IsInFire)
            {
                Destroy(Clone);
                Debug.Log("DELETE");
            }
            else
            {
                items.CoinsSpawned.Add(Clone);
            }

            Speed = GameManager.gameManager.CoinSpeed;
        }
    }
}