using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Chasing");
    }

    public void UpdateState(Enemy enemy)
    {
        // cek player tidak null
        if (enemy.Player != null)
        {
            // Seting destinasi dari enemy ke arah player 
            enemy.NavMeshAgent.destination = enemy.Player.transform.position;

            // cek lagi jarak musuh dengan player, akan pindah state [patrol state] jika jarak lebih dari chase distance
            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) > enemy.ChaseDistance)
            {
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Chasing");
    }
}
