using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType _pickableType;
    public Action<Pickable> OnPicked;
    // karena bertipe action maka bisa dipanggil seperti method

    private void OnTriggerEnter(Collider other)
    {
        // compare tag sangat penting untuk diferensiasi GameObject yang berinteraksi berdasarkan tag
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player mengambil " + _pickableType);
            OnPicked(this);
            Destroy(gameObject);
        }

    }

}
