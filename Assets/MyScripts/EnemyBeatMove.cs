using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDirection
{
    right,left
};


public class EnemyBeatMove : MonoBehaviour {
    private delegate void Movement();
    Movement Moveonbeat;
    public EnemyDirection mydirection;
    public BPMtimer selectedBeatDetection;
    public GameObject leftsensor, rightsensor, leftoutersensor, rightoutersensor;

    // Use this for initialization
    void Start () {
        selectedBeatDetection.OnBPMBeat += OnBeat;
    }
	
	// Update is called once per frame
	void Update () {
        if (mydirection == EnemyDirection.left)
        {            
            if (leftsensor.GetComponent<TriggerSensor>().isInside)
            {
                if (!leftsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().attacking
                 && !leftsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().leftinput)
                {
                    Moveonbeat = leftAttack;
                }
                else if(leftsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().leftinput)
                {
                    Moveonbeat = moveLeft;
                }
                else
                {
                    Moveonbeat = null;
                }
            }
            else
            {
                if (leftoutersensor.GetComponent<TriggerSensor>().isInside && 
                    leftoutersensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().rightinput)
                {
                    Moveonbeat = null;
                }
                else
                {
                    Moveonbeat = moveLeft;
                }
            }
        }
        else if(mydirection == EnemyDirection.right)
        {
            if (rightsensor.GetComponent<TriggerSensor>().isInside)
            {
                if (!rightsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().attacking
                 && !rightsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().rightinput)
                {
                    Moveonbeat = rightAttack;
                }
                else if (rightsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().rightinput)
                {
                    Moveonbeat = moveRight;
                }
                else
                {
                    Moveonbeat = null;
                }
            }
            else
            {
                if (rightoutersensor.GetComponent<TriggerSensor>().isInside &&
                    rightoutersensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().leftinput)
                {
                    Moveonbeat = null;
                }
                else
                {
                    Moveonbeat = moveRight;
                }
            }
        }
    }

    void moveRight()
    {
        this.transform.Translate(1, 0, 0);
    }

    void moveLeft()
    {
        this.transform.Translate(-1, 0, 0);
    }

    void rightAttack()
    {
        rightsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().life--;
    }

    void leftAttack()
    {
        leftsensor.GetComponent<TriggerSensor>().colidedObject.GetComponent<PlayerBeatMovement>().life--;
    }

    void OnBeat()
    {
        if (Moveonbeat != null)
            Moveonbeat();
        Moveonbeat = null;
    }
}
