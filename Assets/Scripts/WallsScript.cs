using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsScript : MonoBehaviour
{

    public static event EventHandler e_WallCollision;

    private void OnCollisionEnter(Collision collision)
    {
        e_WallCollision?.Invoke(this, EventArgs.Empty);
    }
}
