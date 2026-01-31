using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    
    [Header("MOVE SETTINGS")]
    public float maxSpeed;
    public float power;
    public float acc;
    public float dcc;

    [Header("JUMP SETTINGS")]
    public float jumpForce;
    public float coyoteTime = .13f;
    private float coyoteTimeCounter;
    public LayerMask GroundLayer;
}
