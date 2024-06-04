using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIController : MonoBehaviour
{
    public AIDestinationSetter aiPath;

    public void SetAITarget(Transform target)
    {
        if (target != null)
        {
            aiPath.target = target;
        }
        else
        {
            aiPath.target = null;
        }

    }
}
