using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    private void Awake()
    {
        SaveData testData = new SaveData();
        testData.version = 0.1f;
        testData.seed = 0;
        
        testData.deck = new List<int>();

        SaveManager.SaveGame(testData);
    }
}
