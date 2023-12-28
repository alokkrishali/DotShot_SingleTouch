using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour {

    public AnimationCurve movementCurve;
    public float MovementSpeed = 0;
    Transform thisTransform;
    public float EndMovingRange = 11;
    public Vector3 StartingPoint, currentPos;
    public bool IsMoving = false;
    float speedFactor = 0;
    public AIController childController;
    private int AiIndex = 0;
    Rigidbody2D rd2D;

	void Awake ()
    {
        thisTransform = GetComponent<Transform>();
        childController = GetComponentInParent<AIController>();
        rd2D = GetComponent<Rigidbody2D>();
    }
	
    public void SetStationary()
    {
        rd2D.velocity = Vector2.zero;
    }
    public void SetObjectAt(Vector3 pos, float speed, AnimationCurve movingPath, int index)
    {
        thisTransform.position = currentPos = StartingPoint = pos;
        speedFactor = 0;
        MovementSpeed = speed;
        movementCurve = movingPath;
        AiIndex = index;
        StartCoroutine(SetSomeDelay());
    }

    IEnumerator SetSomeDelay()
    {
        yield return new WaitForSeconds(Random.Range(0,3));
        rd2D.freezeRotation = false;
        IsMoving = true;
    }
    public void ResetPos()
    {
        IsMoving = false;
        childController.GetsetCallBack(AiIndex);
        rd2D.velocity = Vector3.zero;
        thisTransform.eulerAngles = Vector3.zero;
        rd2D.freezeRotation = true;
    }

    float velocity = 0, temp = 0;
	void Update ()
    {
        if (IsMoving)
        {
            speedFactor += Time.deltaTime;
            velocity = (3*speedFactor) / EndMovingRange;
            temp = movementCurve.Evaluate(velocity)*2;

            thisTransform.position = new Vector3((currentPos.x - MovementSpeed * speedFactor), currentPos.y + 2*temp, currentPos.z);
            if (thisTransform.position.x < -EndMovingRange)
            {
                ResetPos();
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().CollidedWithEnemy();
            StartCoroutine(ResetBulletAfter());
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            collision.gameObject.GetComponent<Bullet>().BulletHitEffect();
            StartCoroutine(ResetBulletAfter());
        }
    }
    IEnumerator ResetBulletAfter()
    {
        IsMoving = false;
        yield return new WaitForSeconds(1);
        ResetPos();
        yield break;
    }
}
