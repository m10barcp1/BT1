using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    Rigidbody rb;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] float speed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    public LayerMask Wall;
    //[SerializeField] GameObject bullet;
    [SerializeField] float accelaration = 0f;
    Quaternion rotateToTarget;
    float DirRotateValueMax;
    float angle;
    float velocity;
    bool checkCollide = false;
    Vector3 dir;
    Vector3 currentEulerAngles;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

        transform.position = a.position;
        
    }
    void RotateObject()
    {
        dir = b.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotateToTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateToTarget, Time.deltaTime * rotateSpeed);
        //DirRotateValueMax = Vector3.Angle(dir, transform.right);
        
        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,0,DirRotateValueMax));
        //rb.MoveRotation(deltaRotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(a.position, b.position);
    }
    private void FixedUpdate()
    {
        bool canMove = false;
        if(transform.localRotation.x > -90f && transform.localRotation.x < 90f)
        {
            Debug.DrawRay(transform.position, Vector3.right);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f, Wall);
            if(!hit)
            {
                Move();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1f, Wall);
            if (!hit)
            {
                Move();
            }
        }
        
        //b.position = Input.mousePosition;
        RotateObject();
        
    }
    void Move()
    {
        velocity = (speed + accelaration * Time.time) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, b.position, velocity);
    }
}
