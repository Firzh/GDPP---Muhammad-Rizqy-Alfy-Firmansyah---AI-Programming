using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
        enemy.Animator.SetTrigger("PatrolState");
    }

    public void UpdateState(Enemy enemy)
    {
        // cek jika jarak enemy kurang dari chase distance akan pindah state ke chase state
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        
        if (!_isMoving)
        {
            _isMoving = true;
            // random index dari 0 sampai 6
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);

            // Melakukan set destinasi waypoint berdasarkan posisi
            _destination = enemy.Waypoints[index].position;

            // Enemy menuju waypoint sesuai posisi waypoint pilihan
            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            // melakukan cek posisi enemy dan destination untuk menemukan destinasi baru
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}
