using UnityEngine;

public interface IPlayerAnimatorData2D
{
    public Vector2 Move_Live { get; }
    public bool Action1_FixedOnDown { get; }
    public FacingDirection4 FacingDirection { get; }
    public Rigidbody2D Rigidbody2D { get; }
}