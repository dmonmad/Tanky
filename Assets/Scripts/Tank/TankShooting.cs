using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_ProjectileSpeed = 15f;
    public int m_MaxProjectilesAlive = 6;
    public ParticleSystem m_ShootingMuzzle;
    public ParticleSystem m_ShootingSmoke;

    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.
    private JoyButton m_FireButton;

    public LineRenderer m_FireLine;

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
    }


    private void Start()
    {
        // The fire axis is based on the player number.

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_FireButton = FindObjectOfType<JoyButton>();
        m_FireLine.positionCount = 2;
    }


    private void Update()
    {
        // The slider should have a default value of the minimum launch force.
        if (m_FireButton.isPressed() && ShellExplosion.projectiles.Count < m_MaxProjectilesAlive)
        {
            Fire();
            // Change the clip to the charging clip and start it playing.
        }

        if (Physics.Raycast(m_FireTransform.position, m_FireTransform.forward, out RaycastHit hit))
        {
            Debug.DrawRay(m_FireTransform.position, m_FireTransform.forward * 90f, Color.white);
            m_FireLine.SetPosition(0, m_FireTransform.position);
            m_FireLine.SetPosition(1, hit.point);
        }
    }


    private void Fire()
    {
        // Create an instance of the shell and store a reference to it's rigidbody.
        ShellExplosion shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation).GetComponent<ShellExplosion>();

        shellInstance.Initialize(false, 1, m_ProjectileSpeed * m_FireTransform.forward);

        m_ShootingMuzzle.gameObject.SetActive(true);
        m_ShootingSmoke.gameObject.SetActive(true);

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

}