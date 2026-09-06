using UnityEngine;
using TMPro;

public class spaceshipController : MonoBehaviour
{
    [Header("Movement stats")]
    public float speed = 20f;
    public float rotationSpeed = 100f;

    [Header("UI Reference")]
    public TextMeshProUGUI planetText;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float pitchInput = Input.GetAxis("Vertical");
        float yawInput = Input.GetAxis("Horizontal");    

        float pitch = -pitchInput * rotationSpeed * Time.deltaTime;
        float yaw = yawInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(pitch, yaw, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (planetText != null)
        {
            planetText.text = "Hitting: " + other.gameObject.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (planetText != null)
        {
            planetText.text = "Approaching next planet....";
        }
    }
}
