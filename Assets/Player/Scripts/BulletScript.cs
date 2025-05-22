using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Vector3 cursorPos;
    private Camera mainCam;

    private Rigidbody2D rb;
    private float force = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = cursorPos - transform.position;

        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
