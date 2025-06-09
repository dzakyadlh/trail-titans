using Unity.VisualScripting;
using UnityEngine;

public class Tires : MonoBehaviour
{
    [SerializeField] private Rigidbody2D vehicleRb;
    [SerializeField] private float rotationMultiplier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Tires script started!");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardSpeed = Vector2.Dot(vehicleRb.linearVelocity, vehicleRb.transform.right);
        float rotationAmount = forwardSpeed * rotationMultiplier;
        transform.Rotate(0, 0, -rotationAmount);
    }
    
}
