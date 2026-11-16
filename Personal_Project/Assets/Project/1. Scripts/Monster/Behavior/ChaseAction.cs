using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase", story: "[Monster] chase [Player] with [MoveSpeed]", category: "Action", id: "4638ef5ef40e9d5fb7553ffc413c5fb9")]
public partial class ChaseAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Monster;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Vector3 moveDir = (Player.Value.position - Monster.Value.position).normalized;
        Debug.Log(moveDir);
        Monster.Value.Translate(moveDir * MoveSpeed.Value * Time.deltaTime,Space.World);
        Monster.Value.position = new Vector3(Monster.Value.position.x, 0f, 0f);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

