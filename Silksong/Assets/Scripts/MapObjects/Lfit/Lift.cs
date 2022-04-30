using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEngine.Events;
=======
>>>>>>> d279aa9a (update mapObjects)
/// <summary>
/// ����1.0
/// </summary>���ߣ����
public class Lift : MonoBehaviour
{
    public int maxFloor;//��1��ʼ����

<<<<<<< HEAD
    private LiftFloorGear[] gears;//liftΪ�õ��ݵ�liftFloorGear��startʱ�󶨵�������  ���ӵͲ㵽�߲�˳��

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public float currentFloor;//��ǰ�� ��Ϊx.5�����ʾ����x��x+1��֮���ƶ�

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public int targetFloor;//����Ҫ���Ĳ�

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public int midTargetFloor;//���ڲ��Ŀ���֮����м�� �����ڿ����ƶ��м�¼���ݵ�ǰλ��


    private float midFloorHeight;//�м��ĵ���y��߶�
    private float liftFloorDistance;
=======
    public LiftFloorGear[] gears;//liftΪ�õ��ݵ�liftFloorGear��startʱ�󶨵�������  ���ӵͲ㵽�߲�˳��

    public float currentFloor;//��ǰ�� ��Ϊx.5�����ʾ��x��x+1��֮��
    public int targetFloor;//����Ҫ���Ĳ�
    public int midTargetFloor;//���ڲ��Ŀ���֮����м�� �����ڿ����ƶ��м�¼���ݵ�ǰλ��

    public float targetFloorHeight;//Ŀ�����߶�
    public float midFloorHeight;//�м��ĵ���߶�

    public Transform liftFloorTransform;//���ݵذ��λ�� ����ʹ���ݵذ������߶�һ��
>>>>>>> d279aa9a (update mapObjects)

    public float speed;
    private float arriveDistance;//��������Ŀ�ĵؾ���С�ڴ�ֵʱ �ж�����

<<<<<<< HEAD
    // private PlayerController player;
=======
    private PlayerController player;
>>>>>>> d279aa9a (update mapObjects)
    private Rigidbody2D playerRigid;

    private Rigidbody2D rigid;
    private BoxCollider2D floorCollider;//���ݵ������ײ��

<<<<<<< HEAD
#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public bool playerIsOnLift = false;//����Ƿ��ڵ����� ����ͬ���ٶ�

=======
    private bool playerIsOnLift=false;//����Ƿ��ڵ����� ����ͬ���ٶ�
>>>>>>> d279aa9a (update mapObjects)
    void Awake()
    {
        gears = new LiftFloorGear[maxFloor];
        rigid = GetComponent<Rigidbody2D>();
        floorCollider = GetComponent<BoxCollider2D>();

<<<<<<< HEAD
    }
    void Start()
    {
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");
        if (playerobj)
        {
            //player = playerobj.GetComponent<PlayerController>();
            playerRigid = playerobj.GetComponent<Rigidbody2D>();
        }
        arriveDistance = speed * Time.fixedDeltaTime;
        liftFloorDistance = floorCollider.offset.y;
        liftFloorDistance += floorCollider.bounds.extents.y;
    }

    public void setFloorGear(LiftFloorGear gear)
    {
        gears[gear.floor - 1] = gear;
    }

    private float getFloorPosition()
    {
        return transform.position.y + liftFloorDistance;
    }

    private void Update()
    {
        playerIsOnLift = (floorCollider.IsTouchingLayers(1 << LayerMask.NameToLayer("Player")) && playerRigid.transform.position.y > getFloorPosition());

        if (rigid.velocity.y != 0)//�������ƶ�
        {
            float distance = getFloorPosition() - midFloorHeight;
            //Debug.Log(distance);
            if (Mathf.Abs(distance) < arriveDistance)//�ж�����
            {
                // Debug.Log("lift arrive a floor");
                currentFloor = midTargetFloor;//������ĳһ��

                if (midTargetFloor == targetFloor)//����Ŀ�Ĳ�
                {
                    rigid.MovePosition(new Vector3(transform.position.x, transform.position.y - distance, transform.position.z));
                    //�ϸ�������  �����ҵ���ײ������Բ���Բ��ϸ���� ��Ҳ������       
=======
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRigid = player.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        setFloorPosition();
        arriveDistance = speed * Time.fixedDeltaTime;
    }

    private void setFloorPosition()
    {
        float floorDistance= floorCollider.offset.y;
        floorDistance += (floorCollider.size.y / 2);
        floorDistance *= transform.lossyScale.y;
        liftFloorTransform.position = new Vector2(liftFloorTransform.position.x, transform.position.y + floorDistance);
    }

    void FixedUpdate()
    {
        if(rigid.velocity.y!=0)//�������ƶ�
        {
            float distance = liftFloorTransform.position.y - midFloorHeight;
          //  Debug.Log(distance);
            if (Mathf.Abs(distance)< arriveDistance)//�ж�����
            {
               // Debug.Log("lift arrive a floor");
                currentFloor = midTargetFloor;//������ĳһ��
                
                if(midTargetFloor==targetFloor)//����Ŀ�Ĳ�
                {
>>>>>>> d279aa9a (update mapObjects)
                    rigid.velocity = Vector2.zero;
                    if (playerIsOnLift)
                    {
                        playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
<<<<<<< HEAD
                        playerRigid.MovePosition(new Vector2(playerRigid.transform.position.x, playerRigid.transform.position.y - distance));
                        //Debug.Log("stop");
                    }
                }
                else//�����ƶ� ����¥��
=======
                    }
                    rigid.MovePosition(new Vector3(transform.position.x,transform.position.y-distance,transform.position.z));//������� 
                }
                else//�����ƶ�
>>>>>>> d279aa9a (update mapObjects)
                {
                    if (rigid.velocity.y > 0)
                        moveUp();
                    else moveDown();
                }

            }
        }
    }
<<<<<<< HEAD
    void FixedUpdate()
    {

    }
=======
>>>>>>> d279aa9a (update mapObjects)



    /// <summary>
    /// ���ݿ��ؿ��Ƶ��ݵĽӿں���
    /// </summary>
    public void setTargetFloor(int floor)//����ʱ�Ѿ���֤floorһ���Ϸ��Ҳ�����currentfloor
    {

        targetFloor = floor;
<<<<<<< HEAD
=======
        targetFloorHeight = gears[floor - 1].floorHeight;
>>>>>>> d279aa9a (update mapObjects)

        float distance = floor - currentFloor;
        float moveSpeed;
        if (distance > 0)
        {
<<<<<<< HEAD
            moveSpeed = speed;
=======
            moveSpeed = speed;       
>>>>>>> d279aa9a (update mapObjects)
            moveUp();
        }
        else
        {
            moveSpeed = -speed;
            moveDown();
        }

        rigid.velocity = new Vector2(0, moveSpeed);
        if (playerIsOnLift)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, moveSpeed);
<<<<<<< HEAD
            //Debug.Log("with");
        }

    }    //Ŀǰ��һ�ι����򵽶������δ������ ������ܻ��Ļ���ΪĿ��

    public void moveUp()//����һ��
    {
        midTargetFloor = (int)Mathf.Floor(currentFloor) + 1;//����ȡ����+1 ��ʾ��һ�� 
=======
        }

    }    //Ŀǰ��һ�ι����򵽶������δ������ ������ܻ��Ļ���Ŀ��

    public void moveUp()//����һ��
    {
        midTargetFloor = (int)Mathf.Floor(currentFloor)+1;//����ȡ����+1 ��ʾ��һ�� 
>>>>>>> d279aa9a (update mapObjects)
        currentFloor = midTargetFloor - 0.5f;//��ʾ������mid���˶�
        midFloorHeight = gears[midTargetFloor - 1].floorHeight;//��Ӧ¥��ĵ���λ��
    }

    public void moveDown()
    {
        midTargetFloor = (int)Mathf.Ceil(currentFloor) - 1;//����ȡ����-1 ��ʾ��һ�� 
        currentFloor = midTargetFloor + 0.5f;//��ʾ������mid���˶�
        midFloorHeight = gears[midTargetFloor - 1].floorHeight;//��Ӧ¥��ĵ���λ��
    }
<<<<<<< HEAD
}

=======

    private void OnCollisionEnter2D(Collision2D collision)//Ҳ����ʹ��overlap���ж�����Ƿ��ڵ�����
    {
        if(collision.gameObject==player.gameObject && player.isGrounded)
        {
            playerIsOnLift = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {
            playerIsOnLift = false;
        }
    }
}
>>>>>>> d279aa9a (update mapObjects)
