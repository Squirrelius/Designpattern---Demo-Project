using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ZerglingStats")]
public class Stats : ScriptableObject {
    public ZerglingStats zerglingStats;
}

[System.Serializable]
public class ZerglingStats
{
    public int maxHp;
    public float movSpeed;
    private string unitName = "Zergling";
    private int[] itemIDs = new int[100];
}
