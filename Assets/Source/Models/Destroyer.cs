using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : ObjectModel
{
    [SerializeField] string tag;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            other.gameObject.SetActive(false);
        }
    }
}
