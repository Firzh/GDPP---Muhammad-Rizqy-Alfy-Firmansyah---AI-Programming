using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private ScoreManager _scoreManager;
    private List<Pickable> _pickableList = new List<Pickable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        // Menggunakan FindObjectsByType<T>() untuk mendapatkan semua objek Pickable
        Pickable[] _pickableObjects = FindObjectsByType<Pickable>(FindObjectsSortMode.None);

        for (int i = 0; i < _pickableObjects.Length; i++)
        {
            _pickableList.Add(_pickableObjects[i]);
            _pickableObjects[i].OnPicked += OnPickablePicked;
        }

        _scoreManager.SetMaxScore(_pickableList.Count);

        // Debug.Log("Pickable List: " + _pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (_scoreManager != null)
        {
            _scoreManager.AddScore(1);
        }

        if (pickable._pickableType == PickableType.PowerUp)
        {
            Debug.Log("[PickableManager] Player pick POWERUP");
            _player?.PickPowerUp();
        }
        else
        {
            Debug.Log("[PickableManager] Picked item type: " + pickable._pickableType);
        }

        Debug.Log("[PickableManager] Sisa Pickable: " + _pickableList.Count);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("[PickableManager] Win Condition Triggered!");
            SceneManager.LoadScene("WinScene");
        }
    }
}