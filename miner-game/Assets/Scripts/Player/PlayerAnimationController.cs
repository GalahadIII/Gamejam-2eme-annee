using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _sr;
    private IPlayerAnimatorData2D _playerAnimatorData;
    
    
    private string _state;
    private string _lastState;
    // Start is called before the first frame update
    void Awake()
    {
        _playerAnimatorData = GetComponentInParent<IPlayerAnimatorData2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _state = GetCurrentState();
            
        if (_state == _lastState) return;
        //Debug.Log(_state);
        _anim.Play(_state);
        _lastState = _state;
    }
    
    private string GetCurrentState()
    {
        string direction = _playerAnimatorData.FacingDirection.ToString();
        // if (_playerAnimatorData.isDead) _state = "Death"; return
        string movement = Mathf.Abs(_playerAnimatorData.Move_Live.x) > 0.1 || Mathf.Abs(_playerAnimatorData.Move_Live.y) > 0.1 ? "Run" : "Idle";
        
        return movement + direction;
    }

    // public static class PlayerAnimState
    // {
    //     public const string IDLE_DOWN = "IdleDown";
    //     public const string IDLE_LEFT = "IdleLeft";
    //     public const string IDLE_RIGHT = "IdleRight";
    //     public const string IDLE_UP = "IdleUp";
    //     
    //     public const string DAMAGE_DOWN = "DamageDown";
    //     public const string DAMAGE_LEFT = "DamageLeft";
    //     public const string DAMAGE_RIGHT = "DamageRight";
    //     public const string DAMAGE_UP = "DamageUp";
    //     
    //     public const string PICKAXE_DOWN = "PickaxeDown";
    //     public const string PICKAXE_LEFT = "PickaxeLeft";
    //     public const string PICKAXE_RIGHT = "PickaxeRight";
    //     public const string PICKAXE_UP = "PickaxeUp";
    //     
    //     public const string DEATH_DOWN = "DeathDown";
    //     public const string DEATH_LEFT = "DeathLeft";
    //     public const string DEATH_RIGHT = "DeathRight";
    //     public const string DEATH_UP = "DeathUp";
    //     
    //     public const string RUN_DOWN = "RunDown";
    //     public const string RUN_LEFT = "RunLeft";
    //     public const string RUN_RIGHT = "RunRight";
    //     public const string RUN_UP = "RunUp";
    //     
    //     
    // }
}
