using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Wave Details", order = 1)]
public class WaveDetails : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemiesPerRound = 10;
    public int numberOfRounds = 0;
    public float timeBetweenSpawns = 5;
}
