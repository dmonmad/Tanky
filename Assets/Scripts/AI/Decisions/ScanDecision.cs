using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ScanDecision")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool noEnemyInSight = Scan(controller);
        return noEnemyInSight;
    }

    private bool Scan(StateController controller)
    {
        controller.navMeshAgent.isStopped = false;
        controller.transform.Rotate(0, controller.enemyStats.m_RotatingSpeed, 0);
        return controller.CheckIfCountDownElapsed(controller.enemyStats.m_SearchDuration);
    }
}
