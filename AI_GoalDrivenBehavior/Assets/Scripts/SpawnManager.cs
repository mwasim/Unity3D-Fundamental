using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject patientPrefab;
    public int numberOfPatients;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnPatientOnInterval));        
    }

    private IEnumerator SpawnPatientOnInterval()
    {
        for (int i = 0; i < numberOfPatients; i++)
        {
            yield return new WaitForSeconds(3.0f);

            SpawnPatient();
        }        
    }
    
    private void SpawnPatient()
    {
        Instantiate(patientPrefab, transform.position, Quaternion.identity);
    }
}
