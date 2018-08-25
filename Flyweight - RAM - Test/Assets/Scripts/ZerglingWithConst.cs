using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ZerglingWithConst : MonoBehaviour
{
    private float curHp = 100;
    private const int maxHp = 100;
    private const float movSpeed = 1.2f;
    private const string unitName = "Zergling";
    private static readonly int[] itemIDs = new int[100];
}
