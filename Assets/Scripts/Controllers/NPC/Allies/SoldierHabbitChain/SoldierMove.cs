using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : IChainPart
{
    private ISoldierChainHandler _handler;

    private Transform _body;
    private Transform _target;

    private float _raycastDistance;
    private float _raycastSphereRadius;
    private float _attackRadius;

    private float _speed;
    public SoldierMove(ISoldierChainHandler handler, Transform body, float raycastSphereRadius, float raycastDistance, float attackRadius, float speed)
    {
        _attackRadius = attackRadius;
        _handler = handler;
        _body = body;
        _raycastDistance = raycastDistance;
        _raycastSphereRadius = raycastSphereRadius;
        _speed = speed;
    }

    public void Update(double delta)
    {
        if (_handler.GetTarget() == null)
        {
            _handler.UpdateTarget();

            return;
        }
        _target = _handler.GetTarget().transform;

        if ((_target.position - _body.position).sqrMagnitude < _attackRadius * _attackRadius)
        {
            _handler.SetAnimtionWalkSpeed(0);
            _handler.MoveToNext();
        }
        else
        {
            //init
            RaycastHit hit;
            Vector3 fwd = _body.forward;

            if (Physics.SphereCast(_body.position, _raycastSphereRadius, fwd, out hit, _attackRadius))
            {
                if (hit.transform.tag == "Foe" && hit.transform.gameObject.GetComponent<IDestroyable>() != null)
                {
                    _handler.SetAnimtionWalkSpeed(0);
                    _handler.SetTarget(hit.transform.gameObject);
                    _handler.MoveToNext();
                    return;
                }
                else
                {
                    bool isSet = false;
                    Vector3 newDirection = new Vector3(0, 0, 0);
                    Vector3 bufVector = new Vector3();
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == j && i == 0) continue;
                            bufVector.Set(i, 0, j);
                            if (!Physics.SphereCast(_body.position, _raycastSphereRadius, bufVector, out hit, _raycastDistance) && (!isSet || (bufVector - _body.forward).sqrMagnitude < (newDirection - _body.forward).sqrMagnitude))
                            {
                                isSet = true;
                                newDirection.Set(i, 0, j);
                            }
                        }
                    }
                    _handler.SetAnimtionWalkSpeed(1);

                    newDirection.Set(newDirection.x, _body.position.y, newDirection.z);
                    _body.rotation = Quaternion.LookRotation(newDirection);

                    _body.Translate(newDirection * _speed * (float)delta);
                }
            }
            else
            {
                _handler.SetAnimtionWalkSpeed(1);

                Vector3 look = _target.position - _body.position;

                look.y = 0;
                look = look.normalized;
                _body.rotation = Quaternion.LookRotation(look*100);
                _body.Translate(_body.forward * (-1)* _speed * (float)delta);

                _handler.UpdateTarget();
            }
        }
    }
}
