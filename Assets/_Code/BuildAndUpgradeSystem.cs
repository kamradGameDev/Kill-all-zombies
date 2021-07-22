using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAndUpgradeSystem : MonoBehaviour
{
    public static BuildAndUpgradeSystem instance;
    public List<GameObject> buildTowers;
    
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    public void AddNewBuildTower(GameObject _newTower)
    {
        buildTowers.Add(_newTower);
    }
}
