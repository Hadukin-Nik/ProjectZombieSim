using System.Collections.Generic;
using UnityEngine;

public interface IZombiesChainHandler
{
    public void MoveToNext();

    public Dictionary<ZombieMainController, bool> GetZombies();
    public int GetKilled();

    public void AddZombie(ZombieMainController zombie);

    public void RemoveZombie(ZombieMainController zombie);
}
