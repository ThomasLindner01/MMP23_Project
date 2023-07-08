using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;


public class FlowerDND : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool dragged = false;
    Vector2 oldPosition;
    public TMP_Text text;
    int layerMask = 1 << 5;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        int clickID = eventData.pointerId;

        if (dragged == false)
        {
            if (clickID == -1)
            {
                print("Left mouse click registered on: " + name + " " + Input.mousePosition);
                dragged = true;
            }
            else if (clickID == -2)
            {
                Debug.Log("Right mouse click registered on: " + name);
                dragged = true;
            }
        }
    }
    
    
    
    // OnPointerUp (implemented by IPointerUpHandler) is called when a Mousebutton is released.
    // When Mouse is released => check if a Collider is Hit. 
    public void OnPointerUp(PointerEventData eventData)
    {
        print("ponterup");
        dragged = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, layerMask);
        CheckCollision(hit);
        StillMode();
    }

    // If Hit => Check target, react accordingly.
    void CheckCollision(RaycastHit2D hit) 
    {
        if (hit.collider != null)
        {
            Debug.Log("hit:" + hit.collider.name);
            if (hit.collider.name == "PostItContainer")
            {
                text = hit.collider.gameObject.GetComponentInChildren<TMP_Text>();
                text.text = "" + name;
            }
        }
    }
    
    // simple Drag and Drop Loop
    void CheckforDragging()
    {
        if (dragged)    {DraggedMode();}
        else            {StillMode();}
    }
    
    // Transformation of object while dragged
    void DraggedMode()
    {
        this.transform.position = Input.mousePosition;
        this.transform.localScale = (new Vector2 (1,1) / 7 * 6);
    }

    // Default transformation before drag
    void StillMode() 
    {
        this.transform.localPosition = oldPosition;
        this.transform.localScale = (new Vector2(1, 1));
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        CheckforDragging();
    }

}
