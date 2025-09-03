using UnityEngine;

public class PlayerAxisLogger : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Pergerakan player dengan key A,D Left & Right
        float horizontal = Input.GetAxis("Horizontal");

        // Pergerakan player dengan key W,S, Up, & Down
        float vertical = Input.GetAxis("Vertical");

        // vector 3 menyimpan arah gerakan pada 3 sumbu x, y dan z
        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);

        // Normalize the vector to ensure consistent speed in all directions
        if (movementDirection.magnitude > 1)
        {
            movementDirection.Normalize();
        } 

        // Putar arah gerakan agar sesuai dengan rotasi kamera pada sumbu Y
        Vector3 rotatedMovement = _camera.transform.TransformDirection(movementDirection);
        rotatedMovement.y = 0; // Pastikan gerakan hanya pada sumbu X dan Z

        // Menggunakan Rigidbody untuk pergerakan dengan fisika
        _rigidbody.linearVelocity = rotatedMovement * _speed;

        // Tampilkan nilai hanya ketika tombol ditekan (bukan idle = 0)
        // if (Mathf.Abs(horizontal) > 0.01f)
        // {
        //     Debug.Log("Horizontal: " + horizontal.ToString("F3")); // F3 = 3 angka di belakang koma
        // }
        // if (Mathf.Abs(vertical) > 0.01f)
        // {
        //     Debug.Log("Vertical: " + vertical.ToString("F3"));
        // }
    }
}