using System;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public static List<ShellExplosion> projectiles = new List<ShellExplosion>();
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
    public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
    public AudioSource m_RicochetAudio;
    public float m_MaxLifeTime = 10f;                    // The time in seconds before the shell is removed.
    public int m_MaxBouncesCount = 1;
    public int m_BouncesLeft;
    public string m_ProjectileTag = "Projectile";

    private Rigidbody rb;
    private Vector3 lastVelocity;
    private bool m_UseGravity;
    private Vector3 m_Velocity;

    public void Initialize(bool m_ShellGravity, int m_MaxShellBounces, Vector3 velocity)
    {
        m_MaxBouncesCount = m_MaxShellBounces;
        m_UseGravity = m_ShellGravity;
        m_Velocity = velocity;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);

        m_BouncesLeft = m_MaxBouncesCount;
        rb.useGravity = m_UseGravity;
        rb.velocity = m_Velocity;

        lastVelocity = Vector3.zero;
        projectiles.Add(this);
    }

    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool hasHit = false;
        // ... and find their rigidbody.
        Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        // If they don't have a rigidbody, go on to the next collider.
        if (targetRigidbody)
        {
            EnemyTankHealth targetHealth = targetRigidbody.GetComponent<EnemyTankHealth>();
            TankHealth playerHealth = targetRigidbody.GetComponent<TankHealth>();
            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (targetHealth)
            {
                targetHealth.Hit();
                hasHit = true;
            }
            else if (playerHealth)
            {
                playerHealth.Hit();
                hasHit = true;
            }
        }

        if (hasHit || m_BouncesLeft <= 0 || collision.gameObject.CompareTag(m_ProjectileTag))
        {
            Explode();
        }
        else if (!hasHit && m_BouncesLeft > 0)
        {
            ContactPoint contact = collision.contacts[0];
            lastVelocity = Vector3.Reflect(lastVelocity, contact.normal);
            rb.velocity = lastVelocity;
            rb.MoveRotation(Quaternion.LookRotation(lastVelocity));
            m_BouncesLeft--;
            OnRicochet();
        }
    }

    public void Explode()
    {
        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
        Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

        // Destroy the shell.
        Destroy(gameObject);
    }

    private void OnRicochet()
    {
        m_RicochetAudio.Play();
    }

    private void OnDestroy()
    {
        projectiles.Remove(this);
    }

}