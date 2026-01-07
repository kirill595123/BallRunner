using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{ 
    private Rigidbody _rb;
    public UIGame uIGame;
    public items items;
    public Magnit magnit;

    public ParticleSystem Boom;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var Hor = Input.GetAxis("Horizontal");
        var Ver = Input.GetAxis("Vertical");

        Vector3 Move = new Vector3(Hor, 0, Ver);
        _rb.AddForce(Move * 2);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            items.CoinsSpawned.Remove(other.gameObject);
            Destroy(other.gameObject);
            GameManager.gameManager.Score ++;
        }
        else if (other.tag == "Fire" || other.tag == "DealthZone")
        {
            uIGame.GameOver();
        }
        else if (other.tag == "Bomb")
        {
            Destroy(other.gameObject);
            uIGame.GameOver();
        }
        else if (other.tag == "Magnit")
        {
            Destroy(other.gameObject);
            magnit.Colect();
        }
    }
}