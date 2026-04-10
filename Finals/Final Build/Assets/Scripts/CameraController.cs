using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float edgeThickness = 20f;
    private float minX = 23f;
    private float maxX = 39f;
    private float minZ = -4f;
    private float maxZ = 22f;

    void Update()
    {
        Vector3 pos = transform.position;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.mousePosition.y >= Screen.height - edgeThickness) vertical = 1;
        if (Input.mousePosition.y <= edgeThickness) vertical = -1;
        if (Input.mousePosition.x >= Screen.width - edgeThickness) horizontal = 1;
        if (Input.mousePosition.x <= edgeThickness) horizontal = -1;
        pos.x += horizontal * panSpeed * Time.deltaTime;
        pos.z += vertical * panSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}