using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Transform rocket;
    [SerializeField]
    bool IsFireRocket = false;
    [SerializeField]
    float rocketSpeed = 10;

    void Start () {
        StartCoroutine(FireRocket());

	}
	
	// Update is called once per frame
	IEnumerator FireRocket () {
        Vector3 targetpos = playerTransform.position;
        while(IsFireRocket)
        {
            yield return new WaitForSeconds(Random.Range(0, 5));
            GameObject rocketRef = null;
            rocketRef = Instantiate(rocket.gameObject, new Vector3(11, Random.Range(-4, 4), 0), Quaternion.identity) as GameObject;

            if (Vector3.Distance(rocketRef.transform.position, playerTransform.position)>2)
                targetpos = playerTransform.position;
            rocket.transform.position = Vector3.MoveTowards(rocketRef.transform.position, targetpos, Time.deltaTime*rocketSpeed);
            Destroy(rocketRef, 3);
            yield return new WaitForSeconds(.5f);
        }
        
        yield break;
	}
}
