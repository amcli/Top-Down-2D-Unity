using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Vector3 rot = transform. position - cursorPos;

        float rotation = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
