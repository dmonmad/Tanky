using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Rotate")]
public class RotateAction : Action
{
    public override void Act(StateController controller)
    {
        Rotate(controller);
    }

    private void Rotate(StateController controller)
    {
        controller.gameObject.transform.Rotate(0, controller.enemyStats.m_RotatingSpeed * Time.deltaTime, 0);
    }
}
