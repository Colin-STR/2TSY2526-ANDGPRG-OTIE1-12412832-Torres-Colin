using UnityEngine;

public class spaceshipController : MonoBehaviour
{
    [Header("Movement stats")]
    public float speed = 20f;
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float pitchInput = Input.GetAxis("Vertical");
        float yawInput = Input.GetAxis("Horizontal");    

        float pitch = -pitchInput * rotationSpeed * Time.deltaTime;
        float yaw = yawInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(pitch, yaw, 0f, Space.Self);
    }
}
