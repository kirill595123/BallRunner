using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int RotateSpeed;

    void Update()
    {
        transform.Rotate(0f, RotateSpeed * Time.deltaTime, 0f);
    }
}
