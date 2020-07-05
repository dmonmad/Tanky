using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward * 40f, Color.red);

        if (Physics.SphereCast(controller.eyes.position, 1f, controller.eyes.forward, out hit, 40f))
        {
            if (hit.collider.tag.Equals("Player"))
            {
                if (controller.CheckIfCountDownElapsed(controller.enemyStats.m_AttackRate))
                {
                    controller.enemyTankShooting.Fire();
                }
            }
        }
    }
}