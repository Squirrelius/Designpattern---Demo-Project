using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject m_normalZerglingPrefab;
    public GameObject m_constZerglingPrefab;
    public GameObject m_staticZerglingPrefab;
    public GameObject m_flyweightZerglingPrefab;

    private GameObject[] curActiveObjects = new GameObject[10000];

    public void InstantiateObject(GameObject go)
    {
        long bytesAllocatedBefore = Profiler.GetMonoUsedSizeLong();// Profiler.GetTotalAllocatedMemoryLong();
        for (int i = 0; i < 10000; i++)
        {
            Destroy(curActiveObjects[i]);
            curActiveObjects[i] = Instantiate(go);
        }
        long bytesAllocatedAfter = Profiler.GetMonoUsedSizeLong(); // Profiler.GetTotalAllocatedMemoryLong();
        long difference = bytesAllocatedAfter - bytesAllocatedBefore;
        Debug.Log("Allocated a total of " + difference + " for " + go.name);
    }

    public void RunFullTest()
    {
        Debug.Log("normal zergs used: " + Test(m_normalZerglingPrefab, 10) + " of bytes");
        Debug.Log("const zergs used: " + Test(m_constZerglingPrefab, 10) + " of bytes");
        Debug.Log("static zergs used: " + Test(m_staticZerglingPrefab, 10) + " of bytes");
        Debug.Log("flyweight zergs used: " + Test(m_flyweightZerglingPrefab, 10) + " of bytes");
    }

    public long Test(GameObject prefabToInstantiate, int amountOfTests)
    {
        long totalMemoryForPrefab = 0;
        for (int i = 0; i < amountOfTests; i++)
        {
            long bytesAllocatedBefore = Profiler.GetMonoUsedSizeLong();// Profiler.GetTotalAllocatedMemoryLong();

            //First clear the scene
            for (int j = 0; j < 10000; j++)
                Destroy(curActiveObjects[j]);

            //Then instantiate new GameObjects
            for (int j = 0; j < 10000; j++)
                curActiveObjects[j] = Instantiate(prefabToInstantiate);

            long bytesAllocatedAfter = Profiler.GetMonoUsedSizeLong(); // Profiler.GetTotalAllocatedMemoryLong();
            long difference = bytesAllocatedAfter - bytesAllocatedBefore;
            totalMemoryForPrefab += difference;
        }
        totalMemoryForPrefab /= amountOfTests;
        return totalMemoryForPrefab;
    }
}
