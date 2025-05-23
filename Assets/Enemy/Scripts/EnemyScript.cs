using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyScript : MonoBehaviour
{
    public GameObject p;
    public Player pl;
    public FloatingHealthBar healthbar;

    private float speed = 4.0f;
    private float distance;
    private float accelTime = 3.5f;
    private float multiplier;
    private float timer = 0f;

    private float maxHP = 100.0f;
    private float currHP;

    // Start is called before the first frame update
    void Awake()
    {
        currHP = maxHP;
        healthbar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, p.transform.position);
        Vector2 dir = p.transform.position - transform.position;
        dir.Normalize();

        if (distance < 20)
        {
            timer += Time.deltaTime;
            //enemy rotate toward player angle calculation
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            multiplier = timer >= accelTime ? 2.0f : 1f;
            //Debug.Log(multiplier);



            //move enemy toward player
            transform.position = Vector2.MoveTowards(this.transform.position, p.transform.position, speed * multiplier * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }

        else {
            timer = 0f;
            multiplier = 1f;
            Debug.Log(timer);
            Debug.Log(multiplier);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided w/ Player");
            pl.TakeDamage(10);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit by bullet");
            TakeDamage(10);
        }

    }



    public void TakeDamage(float damage)
    {
        currHP -= damage;
        currHP = Mathf.Max(currHP, 0f);
        healthbar.SetHealth(currHP);
        if (currHP == 0)
        {
            Destroy(gameObject);
        }
    }
}
