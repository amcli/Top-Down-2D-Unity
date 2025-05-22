using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyChase : MonoBehaviour
{
    public GameObject p;
    private float speed = 2.0f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, p.transform.position);
        Vector2 dir = p.transform.position - transform.position;
        dir.Normalize();

        //enemy rotate toward player angle calculation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        transform.position = Vector2.MoveTowards(this.transform.position, p.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
}
