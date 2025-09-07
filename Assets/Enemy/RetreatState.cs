using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("[RetreatState] ENTER RETREAT");
        enemy.Animator.SetTrigger("RetreatState");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("[RetreatState] UPDATE RETREAT jalan");
        if (enemy.Player != null)
        {
            Vector3 dir = (enemy.transform.position - enemy.Player.transform.position).normalized;
            enemy.NavMeshAgent.destination = enemy.transform.position + dir * 5f;
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("[RetreatState] EXIT RETREAT");
    }
}
