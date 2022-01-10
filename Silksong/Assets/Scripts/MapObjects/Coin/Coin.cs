using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;

    private float speed = 2f;//��ȡʱ�����ٶ�
    private bool isAttracted = false;//�Ƿ�Ŀ������
    private Vector3 targetPosition;//Ŀ��λ��
    private float jumpForce = 500f;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private int bounceCount;//��������
    [SerializeField] private Vector2 colliderSize;//��ȡ����ײ�д�С

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D.size = colliderSize;
    }
    private void FixedUpdate()
    {
        attract();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (boxCollider2D.IsTouchingLayers(playerLayerMask))//����
        {
            targetPosition = collision.transform.position;
            isAttracted = true;
            rigidbody2D.gravityScale = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(playerLayerMask))
        {
            targetPosition = collision.transform.position;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(playerLayerMask))
        {
            isAttracted = false;
            rigidbody2D.gravityScale = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (circleCollider2D.IsTouchingLayers(playerLayerMask))
        {
            Destroy(gameObject);
        }
    }
    private void Bounce()
    {
        if (IsGround() && !isAttracted && bounceCount > 0)
        {
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            bounceCount -= 1;
        }
    }
    private bool IsGround()
    {
        Vector2 point = (Vector2)circleCollider2D.transform.position + circleCollider2D.offset;
        Collider2D collider = Physics2D.OverlapCircle(point, circleCollider2D.radius, groundLayerMask);
        return collider != null;
    }
    private void attract()
    {
        if (isAttracted)
        {
            transform.Translate((-transform.position + targetPosition) * Time.fixedDeltaTime * speed, Space.World);
        }
    }
}
