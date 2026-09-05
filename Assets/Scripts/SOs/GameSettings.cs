using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }

    [SerializeField] private float jumpVelocity;
    public float JumpVelocity { get { return jumpVelocity; } }


    [Header("Digging")]
    [SerializeField] private float blockSize;
    public float BlockSize { get { return blockSize; } }

    [SerializeField] private float diggingRange;
    public float DiggingRange { get { return diggingRange; } }
}