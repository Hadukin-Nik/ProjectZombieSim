using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHabbitHandler : ISoldierChainHandler, IChainPart
{
    private List<IChainPart> _chains;

    private Transform _body;
    private Transform _target;

    private IBarracks _barracks;

    private Animator _animator;

    private Floor _floor;

    private float _speed, _attackStrength, _attackCoolDown, _sphereRadiusCheck, _maxDistanceReaction, _attackDistance;

    private int _index = 0;

    public SoldierHabbitHandler(GameObject body, IBarracks barracks)
    {
        _floor = GameObject.FindFirstObjectByType<Floor>();

        _body = body.transform;

        _barracks = barracks;

        _animator = body.GetComponent<Animator>();

        SoldierData data = body.GetComponent<SoldierData>();

        _speed = data.GetSpeed();
        _attackStrength = data.GetAttackStrength();
        _attackCoolDown = data.GetAttackCoolDown();
        _sphereRadiusCheck = data.GetSphereRadius();
        _attackDistance = data.GetAttackRadius();
        _maxDistanceReaction = data.GetDistanceTrigger();
        _chains = new List<IChainPart>();

        chainCreate();
    }

    private void chainCreate()
    {
        SoldierTargetLooker targetLooker = new SoldierTargetLooker(this, _body, (int)_maxDistanceReaction);
        SoldierMove soldierMove = new SoldierMove(this, _body, 0.1f, _maxDistanceReaction, _attackDistance, _speed);
        SoldierAttack soldierAttack = new SoldierAttack(this, _body, _attackCoolDown, _attackDistance, _attackStrength);

        _chains.Add(targetLooker);
        _chains.Add(soldierMove);
        _chains.Add(soldierAttack);
    }

    public GameObject GetTarget()
    {
        if (_target == null)
        {
            return null;
        }
        return _target.gameObject;
    }

    public void MoveToNext()
    {
        _index = (_index + 1) % _chains.Count;
    }

    public void SetAnimationAttackSpeed(float speed)
    {
        Debug.Log("Soldier... Attack...");
    }

    public void SetAnimtionWalkSpeed(float speed)
    {
        Debug.Log("Soldier... Walk...");
    }

    public void SetTarget(GameObject target)
    {
        _target = target.transform;
    }

    public void UpdateTarget()
    {
        _index = 0;
    }

    public void Update(double delta)
    {
        if (_target == null) _index = 0;
        _chains[_index].Update(delta);
    }

    public void ReturnToBase()
    {
        
    }

    public GameObject GetBody()
    {
        return _body.gameObject;
    }
}
