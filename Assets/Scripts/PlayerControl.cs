using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public TankShooting shooting;
    public TankHealth health;
    public TankMovement movement;

    public bool m_isPlayerAlive;

    public void TurnOff()
    {
        shooting.enabled = false;
        health.enabled = false;
        movement.enabled = false;
    }

    public void TurnOn()
    {
        shooting.enabled = true;
        health.enabled = true;
        movement.enabled = true;
    }
}
