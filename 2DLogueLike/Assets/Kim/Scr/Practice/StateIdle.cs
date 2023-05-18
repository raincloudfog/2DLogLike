using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : MonoBehaviour,IState
{
   
    public void OperateEnter(Rigidbody2D rigid,Animator anim)
    {
        rigid.velocity = Vector2.zero;
        anim.SetBool("isMove", false);
    }

    public void OperateExit(Collider2D col,)
    {

    }

    public void OperateUpdate()
    {

    }
}
