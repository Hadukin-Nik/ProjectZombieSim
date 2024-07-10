using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealthController : MonoBehaviour, IDestroyable
{
    [SerializeField] private double _health;

    public void AddHealth(double health)
    {
        throw new System.NotImplementedException();
    }

    public bool IsAlive()
    {
        return _health > 0.1f;
    }

    public void RemoveHealth(double health)
    {
        _health -= health;
    }
}
