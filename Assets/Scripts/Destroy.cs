using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1f;

    private void Start()
    {
        Object.Destroy(this.gameObject, lifeTime);
    }
}
