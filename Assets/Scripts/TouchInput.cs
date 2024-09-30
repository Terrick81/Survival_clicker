using System.Runtime.InteropServices;
using UnityEngine;
using YG;

public class TouchInput : MonoBehaviour
{
    public static int current_finger_id = 0;

    public static Vector2 coord;


    void Update()
    {
        if (Constants.mobile) MobileInput();
        else        ComputerInput();
    }

    private void ComputerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            Collider2D objectHit = Physics2D.OverlapPoint(touchPos);
            if (objectHit != null)
            {
                GameObject recipient = objectHit.gameObject;
                recipient.TryGetComponent(out Extracted t);
                t.Send(Input.mousePosition);
                if (t != null)
                {
                    t.Send(Input.mousePosition);
                }
            }
        }
    }
    private void MobileInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    Vector3 wp = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D objectHit = Physics2D.OverlapPoint(touchPos);
                    if (objectHit != null)
                    {
                        GameObject recipient = objectHit.gameObject;
                        recipient.TryGetComponent(out Extracted t);
                        if (t != null)
                        {
                            t.Send(touch.position);
                        }
                    }  
                }
            }
        }
    }

}

/*
                    Vector2 fingerRay = Camera.main.ScreenToWorldPoint(touch.position);
                    Debug.DrawRay(fingerRay, 100*fingerRay, Color.cyan);
                    RaycastHit2D objectHit = Physics2D.Raycast(fingerRay, fingerRay);
                    if (objectHit)
                    {
                        Debug.Log("Touched Finger on GameObject: " + objectHit.collider.name);
                        if (objectHit.collider.name == "myGameObjectName")
                        {
                            Debug.Log("Touched Finger on GameObject: " + objectHit.collider.name);
                        }
                    }
                    */


/*
 ped.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    gr.Raycast(ped, results);

                    foreach (RaycastResult result in results)
                    {
                        GameObject recipient = result.gameObject;
                        Debug.Log(recipient.name);
                        //recipient.SendMessage("OnMouseDown", result.point, SendMessageOptions.DontRequireReceiver);
                    }
*/