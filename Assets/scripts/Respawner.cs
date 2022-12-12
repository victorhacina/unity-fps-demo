using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    public GameObject target;
    public float delay;


    // Start is called before the first frame update
    IEnumerator  Start()
    {
        target.SetActive(false); ;
        yield return new WaitForSeconds(delay);

        var allRespawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        var selectedPoint = allRespawnPoints[Random.Range(0, allRespawnPoints.Length)];

        target.transform.position = selectedPoint.transform.position;
        target.transform.rotation = selectedPoint.transform.rotation;

        target.SetActive(true);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
