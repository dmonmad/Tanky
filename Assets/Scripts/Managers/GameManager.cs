using UnityEngine;
using System.Collections.Generic;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int m_EnemyTanksLeft;
    public GameObject m_LoseMenu;
    public string m_NextLevelName = "Leve1";
    public int m_LevelToUnlock = 0;
    private List<StateController> m_EnemyTanks = new List<StateController>();
    private PlayerControl m_PlayerController;

    //    public int m_NumRoundsToWin = 5;        
    //    public float m_StartDelay = 3f;         
    //    public float m_EndDelay = 3f;           
    //    public CameraControl m_CameraControl;   
    //    public Text m_MessageText;              
    //    public GameObject m_TankPrefab;         
    //    public TankManager[] m_Tanks;           


    //    private int m_RoundNumber;              
    //    private WaitForSeconds m_StartWait;     
    //    private WaitForSeconds m_EndWait;       
    ///*    private TankManager m_RoundWinner;
    //    private TankManager m_GameWinner;       
    //*/
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StateController[] enemyTanks = FindObjectsOfType<StateController>();

        foreach(StateController sc in enemyTanks)
        {
            m_EnemyTanks.Add(sc);
        }

        m_EnemyTanksLeft = enemyTanks.Length;


        m_PlayerController = FindObjectOfType<PlayerControl>();


        Invoke("GameStart", Config.SCENES_WAITINGPREROUND);
        //m_StartWait = new WaitForSeconds(m_StartDelay);
        //m_EndWait = new WaitForSeconds(m_EndDelay);

        //SpawnAllTanks();
        //SetCameraTargets();

        //StartCoroutine(GameLoop());
    }

    public void LoadNextLevel()
    {
        int actualLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex;

        if (actualLevelIndex <= SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        else
        {
            nextLevelIndex = actualLevelIndex + 1;
        }

        string sceneToLoad = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(nextLevelIndex));
        Debug.Log(sceneToLoad);
        
        SceneFader.instance.FadeTo(sceneToLoad);
    }

    private void GameStart()
    {
        StartEnemyTanks();
        m_PlayerController.TurnOn();
    }

    private void StartEnemyTanks()
    {
        foreach(StateController sc in m_EnemyTanks)
        {
            sc.enabled = true;
        }
    }

    private void StopEnemyTanks()
    {
        foreach (StateController sc in m_EnemyTanks)
        {
            sc.enabled = false;

            NavMeshAgent nma = sc.GetComponent<NavMeshAgent>();
            if (nma && nma.isOnNavMesh)
            {
                nma.isStopped = true;
            }
        }
    }

    public void TankDied()
    {
        Debug.Log("Tank died");
        m_EnemyTanksLeft--;
        Debug.Log(m_PlayerController.m_isPlayerAlive);
        if (m_EnemyTanksLeft == 0 && m_PlayerController.m_isPlayerAlive)
        {
            PlayerWon();
        }
        else if((m_EnemyTanksLeft == 0 && !m_PlayerController.m_isPlayerAlive))
        {
            PlayerLost();
        }
    }

    private void StopGame()
    {
        StopEnemyTanks();
        m_PlayerController.TurnOff();
        foreach(ShellExplosion se in ShellExplosion.projectiles)
        {
            se.Explode();
        }
    }

    public void PlayerLost()
    {
        StopGame();
        UILevelManager.instance.ShowLoseMenu();
        m_PlayerController.TurnOff();
    }

    public void PlayerWon()
    {
        StopGame();
        PlayerPrefs.SetInt("levelReached", m_LevelToUnlock);
        LoadNextLevel();
    }

    //    private void SpawnAllTanks()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            m_Tanks[i].m_Instance =
    //                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
    //            m_Tanks[i].m_PlayerNumber = i + 1;
    //            m_Tanks[i].Setup();
    //        }
    //    }


    //    private void SetCameraTargets()
    //    {
    //        Transform[] targets = new Transform[m_Tanks.Length];

    //        for (int i = 0; i < targets.Length; i++)
    //        {
    //            targets[i] = m_Tanks[i].m_Instance.transform;
    //        }

    //        m_CameraControl.m_Targets = targets;
    //    }


    //    private IEnumerator GameLoop()
    //    {
    //        yield return StartCoroutine(RoundStarting());
    //        yield return StartCoroutine(RoundPlaying());
    //        yield return StartCoroutine(RoundEnding());

    ///*        if (m_GameWinner != null)
    //        {
    //            SceneManager.LoadScene(0);
    //        }
    //        else
    //        {
    //            StartCoroutine(GameLoop());
    //        }
    //*/    }


    //    private IEnumerator RoundStarting()
    //    {
    //        yield return m_StartWait;
    //    }


    //    private IEnumerator RoundPlaying()
    //    {
    //        yield return null;
    //    }


    //    private IEnumerator RoundEnding()
    //    {
    //        yield return m_EndWait;
    //    }


    //    private bool OneTankLeft()
    //    {
    //        int numTanksLeft = 0;

    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            if (m_Tanks[i].m_Instance.activeSelf)
    //                numTanksLeft++;
    //        }

    //        return numTanksLeft <= 1;
    //    }

    ///*
    //    private TankManager GetRoundWinner()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            if (m_Tanks[i].m_Instance.activeSelf)
    //                return m_Tanks[i];
    //        }

    //        return null;
    //    }


    //    private TankManager GetGameWinner()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
    //                return m_Tanks[i];
    //        }

    //        return null;
    //    }


    //    private string EndMessage()
    //    {
    //        string message = "DRAW!";

    //        if (m_RoundWinner != null)
    //            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

    //        message += "\n\n\n\n";

    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
    //        }

    //        if (m_GameWinner != null)
    //            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

    //        return message;
    //    }
    //*/

    //    private void ResetAllTanks()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            m_Tanks[i].Reset();
    //        }
    //    }


    //    private void EnableTankControl()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            m_Tanks[i].EnableControl();
    //        }
    //    }


    //    private void DisableTankControl()
    //    {
    //        for (int i = 0; i < m_Tanks.Length; i++)
    //        {
    //            m_Tanks[i].DisableControl();
    //        }
    //    }
}