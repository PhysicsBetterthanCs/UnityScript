using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//此类用于将游戏对象的边界框进行拓展
//以免游戏对象移动出相机或特定边界
//拓展至所有Transform组件的子对象和自身的碰撞件和渲染件
public class Utils : MonoBehaviour
{
    //将边界拓展至另一个对象
    public static Bounds BoundsUnion(Bounds b0,Bounds b1)
    {
        if(b0.size == Vector3.zero && b1.size != Vector3.zero)
        {
            return b1;
        }
        else if (b1.size == Vector3.zero && b0.size != Vector3.zero)
        {
            return b0;
        }
        else if (b0.size == Vector3.zero && b1.size == Vector3.zero)
        {
            return b0;
        }
        b0.Encapsulate(b1.min);
        b0.Encapsulate(b1.max);
        return b0;
    }
    //拓展边界至所有Transform组件子对象和自身的碰撞件和渲染件
    public static Bounds CombineBoundsOfChildren(GameObject go)
    {
        Bounds b = new Bounds(Vector3.zero,Vector3.zero);
        //添加渲染件
        if(go.GetComponent<Renderer>() != null)
        {
            b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
        }
        //添加碰撞件
        if (go.GetComponent<Collider>() != null)
        {
            b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
        }
        foreach(Transform t in go.GetComponent<Transform>())
        {
            b = BoundsUnion(b, CombineBoundsOfChildren(t.gameObject));
        }
        return b;
    }
}
