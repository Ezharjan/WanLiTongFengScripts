using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimation : MonoBehaviour {

    public float MoveSpeed = 0.01f;
    public Vector2 HorizontalOffsetRange = new Vector2 (0.1f, 0.1f);
    public Vector2 VerticalOffsetRange = new Vector2 (0.1f, 0.1f);
    public Vector2 OriginalPos;
    float horizontalSpeed = 1f;
    float verticalSpeed = 1f;

    private void Awake () {
        OriginalPos = transform.position;
    }

    void Update () {
        Vector2 horizontalRange = new Vector2 (OriginalPos.x - HorizontalOffsetRange.x, OriginalPos.x + HorizontalOffsetRange.y);
        Vector2 verticalRange = new Vector2 (OriginalPos.y - VerticalOffsetRange.x, OriginalPos.y + VerticalOffsetRange.y);

        if (HorizontalOffsetRange.magnitude > 0) {
            transform.Translate (new Vector2 (horizontalSpeed, 0) * Time.deltaTime * MoveSpeed);
            if (transform.position.x > horizontalRange.y || transform.position.x < horizontalRange.x)
                horizontalSpeed = -horizontalSpeed;
        }
        if (VerticalOffsetRange.magnitude > 0) {
            transform.Translate (new Vector2 (0, verticalSpeed) * Time.deltaTime * MoveSpeed);
            if (transform.position.y > verticalRange.y || transform.position.y < verticalRange.x)
                verticalSpeed = -verticalSpeed;
        }
    }
}