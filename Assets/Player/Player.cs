using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _powerupDuration;
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _respawnPoint;
    private Coroutine _powerupCoroutine;
    private bool _isPowerUpActive = false;
    private Rigidbody _rigidbody;

    public void PickPowerUp()
    {
        Debug.Log("[Player] PickPowerUp() DIPANGGIL");
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }

        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        Debug.Log("[Player] StartPowerUp: POWERUP ACTIVE");

        if (OnPowerUpStart != null)
        {
            Debug.Log("[Player] OnPowerUpStart event TRIGGER");
            OnPowerUpStart();
        }
        else
        {
            Debug.LogWarning("[Player] OnPowerUpStart TIDAK ADA LISTENER!");
        }

        yield return new WaitForSeconds(_powerupDuration);

        _isPowerUpActive = false;
        Debug.Log("[Player] PowerUp SELESAI");

        if (OnPowerUpStop != null)
        {
            Debug.Log("[Player] OnPowerUpStop event TRIGGER");
            OnPowerUpStop();
        }
        else
        {
            Debug.LogWarning("[Player] OnPowerUpStop TIDAK ADA LISTENER!");
        }

    }

    private void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health: " + _health;
    }

    public void Dead()
    {
        _health -= 1;
        if (_health > 0)
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            UnityEngine.Debug.Log("Lose");
            SceneManager.LoadScene("LoseScene");
        }
        UpdateUI();
    }
}