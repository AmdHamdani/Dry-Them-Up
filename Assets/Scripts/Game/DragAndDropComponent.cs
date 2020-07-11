using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropComponent : MonoBehaviour
{

    private bool isDragging;
    private bool isTouchLine;
    private bool isDrying;
    private float interval;
    private float targetTime;
    private ClothState state;
    private Vector3 defaultPos;
    private Vector3 currentPosition;

    private void Start()
    {
        defaultPos = transform.position;
        state = ClothState.Wet;
        SetInterval(state);
    }

    private void Update()
    {
        if (!isDrying || state == ClothState.Dry) return;

        interval -= Time.deltaTime;

        if(interval <= 0)
        {
            switch(state)
            {
                case ClothState.Wet: state = ClothState.MostlyWet; break;
                case ClothState.MostlyWet: state = ClothState.MostlyDry; break;
                case ClothState.MostlyDry: state = ClothState.Dry; break;
            }

            SetInterval(state);
        }
    }

    private void OnMouseDown() {
        if(Input.GetMouseButton(0))
        {
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        currentPosition.x = Input.mousePosition.ToWorldPosition().x;
        currentPosition.y = Input.mousePosition.ToWorldPosition().y;

        if (isDragging)
        {
            transform.position = currentPosition;
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && !isTouchLine)
        {
            transform.position = defaultPos;
        } else
        {
            isDrying = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Line"))
        {
            isTouchLine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Line"))
        {
            isTouchLine = false;
        }
    }

    private void SetInterval(ClothState state)
    {
        Debug.Log("State is Change to " + state);
        interval = ClothStateInterval.Instance.GetInterval(state);
    }

}
