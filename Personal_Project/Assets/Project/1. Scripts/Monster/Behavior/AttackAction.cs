using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "attack", story: "[Monster] attack with [AttackType]", category: "Action", id: "a5f11c869d72cfaf74fc0b27e6484934")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Monster;
    [SerializeReference] public BlackboardVariable<AttackBase> AttackType;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        AttackType.Value.StartAttack();
        
        return Status.Success;
    }
    
}

