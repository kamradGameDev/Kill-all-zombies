using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<GameObject> enemyPrefabs;
    
    public List<Zombie> zombies;
    private Transform objParentEnemies;
    //public List<GameObject> poolEnemies;
    private void Awake()
    {
        if (!instance) instance = this;
        DontDestroyOnLoad(this);
        objParentEnemies = this.transform.GetChild(0);
    }
    public void ShufflePooled<T>(List<T> _pooled)
    {
        ShuffleArray(_pooled);
    }
    public void InStanceNewZombie(GameObject obj, int _id)
    {
        obj.SetActive(true);
        obj.name = obj.name.Replace("(Clone)", "");
        obj.transform.SetParent(objParentEnemies);
        //poolEnemies.Add(obj);
        zombies.Add(obj.GetComponent<Zombie>());
        obj.GetComponent<Zombie>().id = _id;
        obj.SetActive(false);
    }
    public void ShuffleArray<T>(List<T> _array)
    {
        System.Random _random = new System.Random();
        for(int i = 0; i < _array.Count; i++)
        {
            int j = _random.Next(i + 1);
            var _temp = _array[i];
            _array[j] = _array[i];
            _array[i] = _temp;
        }
    }
}
