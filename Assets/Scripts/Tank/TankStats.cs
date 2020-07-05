using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
public class TankStats : ScriptableObject
{
    public float m_AttackRate = 5f;
    public float m_MovementSpeed = 2f;
    public float m_RotatingSpeed = 2f;
    public float m_SearchDuration = 2f;
    public float m_ProjectileSpeed = 15f;

    public int m_MaxShellBounces = 1;

    public bool m_ProjectileGravity = false;

    public Color m_TankColor = Color.black;
}
