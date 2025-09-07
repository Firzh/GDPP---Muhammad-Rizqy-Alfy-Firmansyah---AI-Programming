using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    public List<Transform> Waypoints = new List<Transform>();

    [SerializeField]
    public float ChaseDistance;

    [SerializeField]
    public Player Player;

    private BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;
    // menentukan destinasi dari AI enemy
    [HideInInspector]
    public Animator Animator;

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = PatrolState;
        _currentState.EnterState(this);

        if (Player != null)
        {
            UnityEngine.Debug.Log("[Enemy] Subscribe ke Player events");
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
        else
        {
            UnityEngine.Debug.LogError("[Enemy] Player BELUM DI-ASSIGN di Inspector!");
        }
    }

    private void StartRetreating()
    {
        UnityEngine.Debug.Log("[Enemy] StartRetreating() DIPANGGIL");
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        UnityEngine.Debug.Log("[Enemy] StopRetreating() DIPANGGIL");
        SwitchState(PatrolState);
    }

    private void Start()
    {
        if (Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != RetreatState)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().Dead();
            }
        }
    }
}
