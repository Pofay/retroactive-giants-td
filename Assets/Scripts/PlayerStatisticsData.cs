using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Starting Stats", menuName = "Level Settings/Starting Stats")]
public class PlayerStatisticsData : ScriptableObject
{
    public int startingCurrency;
    public int startingLives;
}
