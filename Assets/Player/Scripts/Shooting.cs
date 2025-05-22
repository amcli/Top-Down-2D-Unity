using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Shooting : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 cursorPos;

    public GameObject bullet;
    public Transform bulTransform;
    public bool canShoot = true;
    private float timer;
    private float timeBetweenShooting = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = cursorPos - transform.position;
        float rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenShooting)
            {
                canShoot = true;
                timer = 0;
            }

        }
    }

    public void Shoot(InputAction.CallbackContext c)
    {
        if (canShoot)
        {
            canShoot = false;
            Debug.Log("Shooting");
            Instantiate(bullet, bulTransform.position, Quaternion.identity);
        }
    }


}