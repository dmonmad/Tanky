using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Point")]
public class PointAction : Action
{
    public override void Act(StateController controller)
    {
        Point(controller);
    }

    public void Point(StateController controller)
    {
        controller.gameObject.transform.rotation = Quaternion.LookRotation(controller.chaseTarget.position - controller.gameObject.transform.position);
    }
}
