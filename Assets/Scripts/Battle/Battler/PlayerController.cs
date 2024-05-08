using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;//�ړ��X�s�[�h
    int direction = 0;//�ړ�����
    float axisH;//����
    float axisV;//�c��
    public float angleZ = -90.0f;//��]�p�x
    Rigidbody2D rbody;//RigidBody2D
    Animator animator;//Animator
    bool isMoving = false;//�ړ����t���O

    //p1����p2�̊p�x��Ԃ�
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            //�ړ����ł���Ίp�x���X�V����
            //p1����p2�ւ̍����i���_���O�ɂ��邽�߁j
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            //�A�[�N�^���W�F���g2�֐��Ŋp�x�����߂�
            float rad = Mathf.Atan2(dy, dx);
            //���W�A����x�ɕϊ����ĕԂ�
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //��~���ł���Ό��݂̊p�x���ێ�
            angle = angleZ;
        }
        return angle;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

     
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        //�L�[���͂���ړ��p�x�����߂�
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);
        //�ړ��p�x��������Ă�������ƃA�j���[�V�����X�V
        int dir;
        if (angleZ >= -45 && angleZ <= 45)
        {
            //�E����
            dir = 3;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            //�����
            dir = 2;
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            //������
            dir = 0;
        }
        else
        {
            //������
            dir = 1;
        }
        if (dir != direction)
        {
            direction = dir;
            animator.SetInteger("Direction", direction);
        }
    }

    void FixedUpdate()
    {
        //�ړ����x���X�V����
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        if (axisH == 0 && axisV == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
