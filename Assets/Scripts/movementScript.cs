using UnityEditor;
using UnityEngine;

public class movementScript : MonoBehaviour
{

    Vector3 rotation = Vector3.zero;
    public float speed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(1))
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * 2;
        }
        float moveSpeed = 5.0f;
        float moveY = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        float moveX = Input.GetAxis("Horizontal"); // A/D or LEFT/RIGHT arrows
        float moveZ = Input.GetAxis("Jump"); // W/S or Up/Down arrows
        float moveS = Input.GetAxis("Fire1");
        transform.Translate(Vector3.forward * moveY * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * moveX * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * moveZ * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.back * moveZ * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * moveS* moveSpeed * Time.deltaTime);

    }
}
