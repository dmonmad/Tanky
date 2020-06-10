using System;
using UnityEngine;

namespace Complete
{
    public class ShellExplosion : MonoBehaviour
    {
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
        public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
        public float m_MaxLifeTime = 10f;                    // The time in seconds before the shell is removed.
        public int m_MaxBouncesCount = 1;
        public int m_BouncesLeft;

        private void Start()
        {
            // If it isn't destroyed by then, destroy the shell after it's lifetime.
            Destroy(gameObject, m_MaxLifeTime);
            m_BouncesLeft = m_MaxBouncesCount;
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

                // If there is no TankHealth script attached to the gameobject, go on to the next collider.
                if (targetHealth)
                {
                    targetHealth.Hit();
                    hasHit = true;
                }
            }

            if (hasHit || m_BouncesLeft <= 0)
            {
                // Unparent the particles from the shell.
                m_ExplosionParticles.transform.parent = null;

                // Play the particle system.
                m_ExplosionParticles.Play();

                // Play the explosion sound effect.
                m_ExplosionAudio.Play();

                RaycastHit hit;
                if (!Physics.Raycast(transform.position, -transform.up, out hit, 20f))
                {
                    return;
                }

                PaintSplat();

                // Once the particles have finished, destroy the gameobject they are on.
                ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
                Destroy(m_ExplosionParticles.gameObject, mainModule.duration);

                // Destroy the shell.
                Destroy(gameObject);
            }
            else if (!hasHit && m_BouncesLeft > 0)
            {
                m_BouncesLeft--;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            

            
        }

        private void PaintSplat()
        {


        }
    }
}