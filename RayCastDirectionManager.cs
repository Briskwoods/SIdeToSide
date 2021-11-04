using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastDirectionManager : MonoBehaviour
{
    [SerializeField] private BoardCharacterController m_self;

    public int m_upDetectCount = 0;
    public int m_downDetectCount = 0;
    public int m_leftDetectCount = 0;
    public int m_rightDetectCount = 0;

    [Header("Directions Player Can Move")]
    public bool UP;
    public bool DOWN;
    public bool LEFT;
    public bool RIGHT;

    [Header("Jump To Destination")]
    public Transform Up_Target;
    public Transform Down_Target;
    public Transform Left_Target;
    public Transform Right_Target;

    [Header("Raycast LayerMask")]
    public LayerMask layerMask;

    [Space(5)]
    public float HitDistance;

    [Space(5)]
    public float RayCastYPos;


    private void Start()
    {
        m_self = this.gameObject.GetComponent<BoardCharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 RayCastOrigin = transform.localPosition;
        RayCastOrigin.y = RayCastYPos;
        

        //DOWN
        RaycastHit hit_1;
        if (Physics.Raycast(RayCastOrigin, transform.InverseTransformDirection(Vector3.forward), out hit_1, Mathf.Infinity, layerMask))
        {
            switch (hit_1.transform.GetComponent<Availability>().SlotAvailable)
            {
                case true:
                    DOWN = true;
                    break;
                case false:
                    DOWN = false; 
                    break;
            }

            if (hit_1.transform.GetComponent<Availability>().SlotAvailable && m_downDetectCount == 0 && !m_self.isMoving) 
            {
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.forward) * HitDistance, Color.red); 
                //DOWN = true; 
                Down_Target = hit_1.transform;
                m_self.m_down = hit_1.transform;
                m_self.b_canMoveDown = true;
                m_downDetectCount = 1;
                //if (!m_self.isMoving && m_self.b_canMoveDown)
                //{
                //}
            }
            else 
            {
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.forward) * HitDistance, Color.black);
                //DOWN = false; 
                Down_Target = null;
                //if (!m_self.isMoving && m_self.b_canMoveDown)
                //{
                //    m_self.b_canMoveDown = false;
                //}
            }
        }
        else 
        { 
            Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.forward) * HitDistance, Color.black);
            DOWN = false;
            Down_Target = null;
            //if (!m_self.isMoving && m_self.b_canMoveDown)
            //{
            //    m_self.b_canMoveDown = false;
            //}
        }



        //UP
        RaycastHit hit_2;
        if (Physics.Raycast(RayCastOrigin, transform.InverseTransformDirection(Vector3.back), out hit_2, Mathf.Infinity, layerMask))
        {
            switch (hit_2.transform.GetComponent<Availability>().SlotAvailable)
            {
                case true:
                    UP = true;
                    break;
                case false:
                    UP = false;
                    break;
            }
            if (hit_2.transform.GetComponent<Availability>().SlotAvailable && m_upDetectCount == 0 && !m_self.isMoving) 
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.back) * HitDistance, Color.red);
                //UP = true;
                Up_Target = hit_2.transform;
                m_self.m_up = hit_2.transform;
                m_self.b_canMoveUp = true;
                m_upDetectCount = 1;
                //if (!m_self.isMoving && m_self.b_canMoveUp)
                //{
                //}
            }
            else 
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.back) * HitDistance, Color.black);
                //UP = false; 
                Up_Target = null;
                //if (!m_self.isMoving && m_self.b_canMoveUp)
                //{
                //    m_self.b_canMoveUp = false;
                //}
            }
        }
        else 
        { 
            Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.back) * HitDistance, Color.black);
            UP = false;
            Up_Target = null;
            //if (!m_self.isMoving && m_self.b_canMoveUp)
            //{
            //    m_self.b_canMoveUp = false;
            //}
        }



        //RIGHT
        RaycastHit hit_3;
        if (Physics.Raycast(RayCastOrigin, transform.InverseTransformDirection(Vector3.left), out hit_3, Mathf.Infinity, layerMask))
        {
            switch (hit_3.transform.GetComponent<Availability>().SlotAvailable)
            {
                case true:
                    RIGHT = true;
                    break;
                case false:
                    RIGHT = false;
                    break;
            }

            if (hit_3.transform.GetComponent<Availability>().SlotAvailable && m_rightDetectCount == 0 && !m_self.isMoving) 
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.left) * HitDistance, Color.red); 
                Right_Target = hit_3.transform;
                m_self.m_right = hit_3.transform;
                m_self.b_canMoveRight = true;
                m_rightDetectCount = 1;

                //if (!m_self.isMoving && m_self.b_canMoveRight)
                //{
                //}
            }
            else 
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.left) * HitDistance, Color.black);
                //RIGHT = false; 
                Right_Target = null;
                //if (!m_self.isMoving && m_self.b_canMoveRight)
                //{
                //    m_self.b_canMoveRight = false;
                //}
            }
        }
        else 
        { 
            Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.left) * HitDistance, Color.black);
            RIGHT = false;
            Right_Target = null;
            //if (!m_self.isMoving && m_self.b_canMoveRight)
            //{
            //    m_self.b_canMoveRight = false;
            //}
        }



        //LEFT
        RaycastHit hit_4;
        if (Physics.Raycast(RayCastOrigin, transform.InverseTransformDirection(Vector3.right), out hit_4, Mathf.Infinity, layerMask))
        {
            switch (hit_4.transform.GetComponent<Availability>().SlotAvailable)
            {
                case true:
                    LEFT = true;
                    break;
                case false:
                    LEFT = false;
                    break;
            }

            if (hit_4.transform.GetComponent<Availability>().SlotAvailable && m_leftDetectCount == 0 && !m_self.isMoving)
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.right) * HitDistance, Color.red);
                //LEFT = true; 
                Left_Target = hit_4.transform;
                m_self.m_left = hit_4.transform;
                m_self.b_canMoveLeft = true;
                m_leftDetectCount = 1;

                //if (!m_self.isMoving && m_self.b_canMoveLeft)
                //{
                //}
            }
            else 
            { 
                Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.right) * HitDistance, Color.black); 
                //LEFT = false;
                Left_Target = null;
                //if (!m_self.isMoving && m_self.b_canMoveLeft)
                //{
                //    m_self.b_canMoveLeft = false;
                //}
            }
        }
        else
        { 
            Debug.DrawRay(RayCastOrigin, transform.InverseTransformDirection(Vector3.right) * HitDistance, Color.black);
            LEFT = false;
            Left_Target = null;
            //if (!m_self.isMoving && m_self.b_canMoveLeft)
            //{
            //    m_self.b_canMoveLeft = false;
            //}
        }
    }



    public void Reset()
    {
        m_upDetectCount = 0;
        m_downDetectCount = 0;
        m_leftDetectCount = 0;
        m_rightDetectCount = 0;
    }
}
