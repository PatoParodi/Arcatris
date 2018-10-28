using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddleAutomatico : MonoBehaviour {

    public bool isActivo = false;
    public float maxDistance = 0.25f;

    GameController controller;

	// Use this for initialization
	void Start () {

        controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();

	}
	
	// Update is called once per frame
	void Update () {

        if (isActivo)
        {
            if (controller.pelotaViva != null)
            {
                if (controller.ballInPlay)
                {

                    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(controller.pelotaViva.transform.position.x + Random.Range(-0.5f,0.5f), transform.position.y, 0), maxDistance);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(controller.pelotaViva.transform.position.x, transform.position.y, 0), maxDistance);

                }
            }
        }
	}

}
