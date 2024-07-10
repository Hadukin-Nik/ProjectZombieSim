using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksMainController : MonoBehaviour, IHordAction, IBarracks
{
    [SerializeField] private GameObject _soldierPrefab;
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private Vector3 _soldierSize;
    [SerializeField] private int _spawnCount;

    private bool _setToDestroy = false;

    private IDestroyable _healthController;
    private BuildingContoller _buildingContoller;
    private Floor _floor;

    private HashSet<SoldierMainController> _soldiers;

    // Start is called before the first frame update
    void Start()
    {
        _healthController = this.GetComponent<IDestroyable>();
        _buildingContoller = this.GetComponent<BuildingContoller>();
        _floor = FindAnyObjectByType<Floor>();
        _soldiers = new HashSet<SoldierMainController> ();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var soldier in _soldiers)
        {
            soldier.Update(Time.deltaTime);
        }

        if (_setToDestroy)
        {
            _setToDestroy = false;
            OnEnd();
            if (_buildingContoller.getUsedPoints() != null && _buildingContoller.getUsedPoints().Count > 0)
            {
                _floor.ReleaseBuildingPoints(_buildingContoller);
            }
            Destroy(gameObject);
        }

        if (!_healthController.IsAlive() && !_setToDestroy)
        {
            _setToDestroy = true;
        }
    }

    public void OnStart()
    { 
        Vector3 z = new Vector3(0, 0, _soldierSize.z + 0.4f);
        for (int i = 0; i < _spawnCount; i++)
        {
            GameObject instance = GameObject.Instantiate(_soldierPrefab, _spawnPlace.position + z * i, _soldierPrefab.transform.rotation);

            SoldierHabbitHandler soldierHabbitHandler = new SoldierHabbitHandler(instance, this);
            SoldierMainController soldierMainController = new SoldierMainController(soldierHabbitHandler, this, instance.GetComponent<IDestroyable>(), instance);

            _soldiers.Add(soldierMainController);
        }
    }

    public void OnEnd()
    {
        List<GameObject> onDestroy = new List<GameObject>();

        foreach(var i  in _soldiers)
        {
            onDestroy.Add(i.GetGameObject());
        }

        _soldiers.Clear();

        foreach(var i in onDestroy)
        {

            Destroy(i);
        }
    }

    public void AddDeadSoldier(SoldierMainController soldier)
    {
        Debug.Log("Trying to kill");
        _soldiers.Remove(soldier);
    }

    public void AddAliveSoldier(SoldierMainController soldier)
    {
        AddDeadSoldier(soldier);
    }
}
