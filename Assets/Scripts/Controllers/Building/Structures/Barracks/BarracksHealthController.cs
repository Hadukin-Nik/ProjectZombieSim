using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksHealthController : MonoBehaviour, IDestroyable
{
    [SerializeField] private float _initialHealth = 1000;
    [SerializeField] private float _health;


    public void Start()
    {
        _health = _initialHealth;

    }

    public void AddHealth(double health)
    {
        _health = Mathf.Min(_health + (float)health, _initialHealth);
    }

    public bool IsAlive()
    {
        return _health > 0.01;
    }

    public void RemoveHealth(double health)
    {
        _health -= (float)health;
    }

}
