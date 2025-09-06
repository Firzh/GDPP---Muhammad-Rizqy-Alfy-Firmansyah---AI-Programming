using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableManager : MonoBehaviour
{
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

        // Debug.Log("Pickable List: " + _pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Debug.Log("Pickable List: " + _pickableList.Count);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}