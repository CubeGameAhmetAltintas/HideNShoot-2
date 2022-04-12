using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairForce : MonoBehaviour
{
    [SerializeField] Rigidbody[] rbs;
    [SerializeField] Vector3 dir;
    [SerializeField] float force;

    private void Update()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].AddForce(dir * force);
        }
    }

}
