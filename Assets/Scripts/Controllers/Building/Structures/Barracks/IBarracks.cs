using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBarracks 
{
    public void AddDeadSoldier(SoldierMainController soldier);
    public void AddAliveSoldier(SoldierMainController soldier);
}
