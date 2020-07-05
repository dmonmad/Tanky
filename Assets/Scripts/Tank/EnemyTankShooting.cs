using UnityEngine;
using UnityEngine.UI;

public class EnemyTankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_ProjectileSpeed = 15f;
    public int m_MaxProjectilesAlive = 6;
    public int m_MaxShellBounces = 1;
    public bool m_ShellGravity = false;

    public void Fire()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        ShellExplosion exp = shellInstance.GetComponent<ShellExplosion>();

        exp.Initialize(m_ShellGravity, m_MaxShellBounces, m_ProjectileSpeed * m_FireTransform.forward);
        // Set the shell's velocity to the launch force in the fire position's forward direction.
        //shellInstance.velocity = m_ProjectileSpeed * m_FireTransform.forward;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

}