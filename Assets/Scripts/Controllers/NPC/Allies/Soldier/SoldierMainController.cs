using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMainController : IChainPart
{
    private SoldierHabbitHandler _handler;
    private IBarracks _barracks;
    private IDestroyable _soldierHealthController;

    private GameObject _soldier;

    public SoldierMainController(SoldierHabbitHandler handler, IBarracks barracks, IDestroyable soldierHealthController, GameObject soldier)
    {
        _barracks = barracks;
        _handler = handler;
        _soldierHealthController = soldierHealthController;
        _soldier = soldier;
    }

    public void Update(double delta)
    {
        if(_soldier == null)
        {
            return;
        }

        if (_soldierHealthController.IsAlive())
        {
            _handler.Update(delta);
        }
        else
        {
            _barracks.AddDeadSoldier(this);

            GameObject.Destroy(_soldier);
        }
    }

    public GameObject GetGameObject()
    {
        return _soldier;
    }
}
