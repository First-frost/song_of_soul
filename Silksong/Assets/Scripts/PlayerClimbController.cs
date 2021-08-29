using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbController : MonoBehaviour
{
    // Ĭ��ֵ ����Ǩ�Ƶ������ļ�
    public int ConfigGravity = 5;
    public int ConfigClimbSpeed = 60;
    public float ConfigCheckRadius = 0.3f;

    private bool m_isClimb;
    private Rigidbody2D m_rb;

    private Transform m_ropeCheck;
    private LayerMask m_ropeLayer;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_ropeLayer = LayerMask.GetMask("Rope");
        m_ropeCheck = transform.Find("RopeCheck");
    }

    // Update is called once per frame
    void Update()
    {
        // �ж��Ƿ���������
        if (Physics2D.OverlapCircle(m_ropeCheck.position, ConfigCheckRadius, m_ropeLayer))
        {
            // �����ϼ���ʼ����
            if (Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.W))
            {
                OnClimb();
            }

            // �¼�����Ծ��ȡ������
            if (Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.Z))
            {
                OnUnclimb();
            }
        }
        // ����ȡ������
        else
        {
            OnUnclimb();
        }
    }

    private void OnClimb ()
    {
        // ����ʱȡ����ɫ����
        m_rb.velocity = Vector3.zero;

        // ��������У���λ����������
        if (m_isClimb)
        {
            Vector2 pos = transform.position;
            pos += (ConfigClimbSpeed * Vector2.up * Time.deltaTime);
            m_rb.MovePosition(pos);
        }
        // �л�������״̬ ������������Ϊ0
        else
        {
            m_rb.gravityScale = 0;
            m_isClimb = true;
        }
    }

    private void OnUnclimb()
    {
        // ȡ������״̬ �ָ�����
        if (m_isClimb)
        {
            m_rb.gravityScale = ConfigGravity;
            m_isClimb = false;
        }
    }
}
