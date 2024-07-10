using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : IChainPart
{
    private Transform _body;
    private ISoldierChainHandler _handler;
    private IDestroyable _target;
    private float _attackRecharge;
    private float _attackStrength;
    private float _attackDistance;

    private float _attackCoolDown;
    public SoldierAttack(ISoldierChainHandler handler, Transform body, float attackTime, float attackStrength, float attackDistance)
    {
        _body = body;
        _handler = handler;
        _attackDistance = attackDistance;
        _attackRecharge = attackTime;
        _attackStrength = attackStrength;

        _attackCoolDown = 0;
    }

    public void Update(double delta)
    {
        if (_handler.GetTarget() == null)
        {
            _handler.SetAnimationAttackSpeed(0);
            _handler.MoveToNext();
            return;
        }
        _target = _handler.GetTarget().GetComponent<IDestroyable>();
        Vector3 position = _handler.GetTarget().transform.position;
        if (_target.IsAlive() && (_handler.GetTarget().transform.position - _handler.GetBody().transform.position).sqrMagnitude < _attackDistance * _attackDistance)
        {
            if (_attackCoolDown <= 0)
            {
                _handler.SetAnimationAttackSpeed(1);

                Vector3 look = position - _body.position;
                look.y = _body.position.y;
                _body.rotation = Quaternion.LookRotation(look);
                _target.RemoveHealth(_attackStrength);
                _attackCoolDown = _attackRecharge;
            }
            _attackCoolDown -= (float)delta;
        } else
        {
            _handler.SetAnimationAttackSpeed(0);

            _handler.UpdateTarget();
        }
    }
}
