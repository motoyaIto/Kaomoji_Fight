using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gizmo : MonoBehaviour {

    [SerializeField]
    private float gizmoSize = 0.3f;//ギズモの大きさ
    [SerializeField]
    private Color gizmoColor = Color.yellow;//ギズモの色

    [SerializeField]
    private float Flick_spd = 0.1f;//ページをめくるスピード

    private void Start()
    {
        //プラス値に修正
        if(Flick_spd < 0)
        {
            Flick_spd *= -1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    /// <summary>
    /// めくる
    /// </summary>
    /// <param name="spd">めくるスピード</param>
    private void Flick(float spd)
    {
        Quaternion Gizmo_move = Quaternion.identity;

        Gizmo_move = this.transform.rotation;
        Gizmo_move.y += Flick_spd;
        this.transform.rotation = Gizmo_move;
    }

    /// <summary>
    /// ページをめくる
    /// </summary>
    /// <param name="Flickpage">ページをめくる(true)戻す(false)</param>
    /// <returns>めくっている(true)めくってない(false)</returns>
    public bool Flickpage(bool Flickpage)
    {
        //次のページへ
        if (Flickpage == true)
        {
            //ページをめくり終わったら
            if (Mathf.Abs(this.transform.rotation.y) >= 1.0f)
            {
                return false;
            }

            Flick(Flick_spd);
        }
        //前のページへ
        else
        {
            //ページをめくり終わったら
            if (Mathf.Abs(this.transform.rotation.y) <= 0.0f)
            {
                return false;
            }

            Flick(Flick_spd * -1);
        }

        return true;
    }
}
