using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    Camera cam;
    [SerializeField]
    Vector2 touchPos = Vector2.zero, targetPos = Vector2.zero;
    [SerializeField]
    bool IsRotate = false, IsSettle = true;
    [SerializeField]
    float offsetValue, Speed = 5;
    [SerializeField]
    int factor = 0;
    [SerializeField]
    AnimationCurve pathChangeCurve;
    [SerializeField]
    Transform particle;

    [SerializeField]
    FireBullet fireSyatem;

    Transform thisTransform;

    void Start ()
    {
        thisTransform = GetComponent<Transform>();
        targetPos = thisTransform.position;
    }

    public void DoAction(Vector2 pos, int horizTouch = 0, int vertTouch = 0)
    {
        touchPos = pos;
        SetPlayerTargetPos(vertTouch);
        if(horizTouch==2)
        {
            particlePos = cam.ScreenToWorldPoint(pos);
            particle.position = particlePos;
            touched = particlePos;

            if (particleEnabling != null)
                StopCoroutine(particleEnabling);
            particleEnabling = StartCoroutine("TouchEffect");
            IsRotate = true;
        }
    }

    Coroutine particleEnabling;
    float particleSizefactor = 0;
    Vector2 particlePos;
    IEnumerator TouchEffect()
    {
        particleSizefactor = 0;
        particle.gameObject.SetActive(true);
        while (particleSizefactor<1)
        {
            particleSizefactor += 4*Time.deltaTime;
            particle.localScale = new Vector3(particleSizefactor, 1, particleSizefactor);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.3f);
        while (particleSizefactor > 0)
        {
            particleSizefactor -= 4*Time.deltaTime;
            particle.localScale = new Vector3(particleSizefactor, 1, particleSizefactor);
            yield return new WaitForEndOfFrame();
        }
        particle.gameObject.SetActive(false);

    }

    void SetPlayerTargetPos(int vertFactor)
    {
        targetPos = new Vector2(targetPos.x, offsetValue* CalculateVerticalMultiPlier(vertFactor));
        IsSettle = false;
    }
    int CalculateVerticalMultiPlier(int value)
    {
        if(value == 1)
        {
            factor++;
            if (factor > 1)
                factor = 1;
        }
        else if(value == 2)
        {
            factor--;
            if (factor < -1)
                factor = - 1;
        }
        curvef = 0;
        return factor;
    }
    [SerializeField]
    float turningpath = 0, curvef=0;
    Vector2 touched;
    private void Update()
    {
        if(!IsSettle)
        {
            curvef += Time.deltaTime;
            turningpath = pathChangeCurve.Evaluate(curvef);
            thisTransform.position = Vector2.MoveTowards(thisTransform.position, targetPos, Speed* turningpath);
            if(Mathf.Approximately(thisTransform.position.y, targetPos.y))
            {
                IsSettle = true;
                thisTransform.position = targetPos;
            }
        }
        if (IsRotate)
        {
            LookAtTheTouch();
            IsRotate = false;
        }
    }

    //[SerializeField]
    //float rotationSpeed = 10, angle = 0;
    //[SerializeField]
    //Vector2 dir;
    private void LookAtTheTouch()
    {
        fireSyatem.FireBullets(particle.position);
    }
    public void CollidedWithEnemy()
    {
        Debug.Log("Helth Reduce or die");
    }
}
