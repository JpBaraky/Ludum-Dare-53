using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggable : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public RectTransform RectTransform;
    private playerScript playerScript;
    private Rigidbody2D Rigidbody2D;
    public GameObject text;

    private void Start() {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;
    }
    private Vector3 GetMouseWorldPosition() {

        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ;
    }
    private void OnMouseDown() {
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
      
       this.RectTransform.pivot = gameObject.transform.position - GetMouseWorldPosition();
        playerScript.onMouse = true;
    }
    private void OnMouseDrag() {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
        text.transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
    private void OnMouseUp() {
        Rigidbody2D.velocity= Vector3.zero;
        playerScript.onMouse = false;
    }
}
