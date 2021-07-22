using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret Data", menuName = "Turret Data/ New")]
public class DataTurret : ScriptableObject
{
    public int maxLevel = 18;
    public string typeTurret;
    public Sprite iconTurret;
    public float[] timeUpgrade;
    public float[] damageLevel;
    public float[] rangeAttack;
    public float[] speedAttackLevel;
    public string[] skillLevel;
    public int[] upgradeLevelPriceTurrets;
}
