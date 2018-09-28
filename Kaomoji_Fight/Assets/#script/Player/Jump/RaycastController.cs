using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{

    public LayerMask collisionMask;         //衝突マスク

    public const float skinWidth = .015f;   //
    public int horizontalRayCount = 4;      //横の点の数
    public int verticalRayCount = 4;        //縦の点の数

    [HideInInspector]
    public float horizontalRaySpacing;      //横の例を表示する間隔
    [HideInInspector]
    public float verticalRaySpacing;        //縦のrayを表示する間隔

    [HideInInspector]
    public BoxCollider2D collider;          //衝突したオブジェクトのあたり判定
    public RaycastOrigins raycastOrigins;   //rayの原点

    public virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    /// <summary>
    /// 各rayの原点の更新
    /// </summary>
    public void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }


    /// <summary>
    /// rayの点と点の間を計算する
    /// </summary>
    public void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;    //衝突したオブジェクトのAABBを取得
        bounds.Expand(skinWidth * -2);      //？？？

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);  //横の点の数の最小と最大を制限する
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);      //縦の点の数の最小と最大を制限する

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);//横の点の間隔を調整
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);    //縦の点の間隔を調整
    }

    /// <summary>
    /// rayの原点
    /// </summary>
    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}