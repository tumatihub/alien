using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
