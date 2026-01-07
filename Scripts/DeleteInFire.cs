using UnityEngine;

public class DeleteInFire : MonoBehaviour
{
   public items items; 

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire")
        {
            Destroy(gameObject);
            items.CoinsSpawned.Remove(other.gameObject);
        }
    }
}
