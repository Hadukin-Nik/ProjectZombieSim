using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierChainHandler
{
    public void SetTarget(GameObject target);
    public GameObject GetTarget();
    public GameObject GetBody();

    public void MoveToNext();
    public void UpdateTarget();
    public void ReturnToBase();

    public void SetAnimationAttackSpeed(float speed);

    public void SetAnimtionWalkSpeed(float speed);
}
