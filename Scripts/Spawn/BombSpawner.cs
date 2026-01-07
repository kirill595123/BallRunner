using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject BombPrefab;

    private float Speed;

    public items items;

    void Start()
    {
        Speed = GameManager.gameManager.BombSpeed;
    }

    void Update()
    {
        Speed -= Time.deltaTime;

        if (Speed <= 0)
        {
            Vector3 RandomPos = new Vector3(Random.Range(-6, 6), 0.4f, Random.Range(-6,6));

            var Clone = Instantiate(BombPrefab, RandomPos, Quaternion.identity);

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
            }
            else
            {
                items.BombSpawned.Add(Clone);
            }
            Speed = GameManager.gameManager.BombSpeed;
        }
    }
}