using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HorizontalTouchPos { None = 0, Left, Right}
public enum VerticleTouchPos { None = 0, Up, Down, Middle }

public class InputController : MonoBehaviour {

    public HorizontalTouchPos horizPos;
    public VerticleTouchPos vertPos;

    Vector2 screenDimension;
    public Vector2 mousePosition;

    PlayerController thisController;

    void Start ()
    {
        screenDimension = new Vector2(Screen.width, Screen.height);
        thisController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Touch mytouch = Input.GetTouch(0);
        //Touch[] myTouches = Input.touches;
        ////for()
        //{

        //}
		if(Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            HorizontalTouch();
            VerticleTouch();
            thisController.DoAction(mousePosition, (int) horizPos, (int) vertPos);
        }
	}
    void HorizontalTouch()
    {
        if (mousePosition.x > screenDimension.x / 2)
        {
            horizPos = HorizontalTouchPos.Right;
        }
        else if(mousePosition.x < screenDimension.x / 2)
        {
            horizPos = HorizontalTouchPos.Left;
        }
        else
        {
            horizPos = HorizontalTouchPos.None;
        }
    }
    void VerticleTouch()
    {
        if (horizPos == HorizontalTouchPos.Right)
        {
            vertPos = VerticleTouchPos.None;
            return;
        }

        if (mousePosition.y > screenDimension.y / 2)
        {
            vertPos = VerticleTouchPos.Up;
        }
        else if (mousePosition.y < screenDimension.y / 2)
        {
            vertPos = VerticleTouchPos.Down;
        }
        else
        {
            vertPos = VerticleTouchPos.None;
        }
    }

}
