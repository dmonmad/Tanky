using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward * 40f);

        if (Physics.SphereCast (controller.eyes.position, 1f, controller.eyes.forward, out hit, 40f))
        {
            if(hit.collider.tag.Equals("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
