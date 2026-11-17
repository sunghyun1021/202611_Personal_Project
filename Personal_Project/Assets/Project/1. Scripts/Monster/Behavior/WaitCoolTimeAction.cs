using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitCoolTime", story: "[Monster] stop until [CoolTime] with [Animator]", category: "Action", id: "7ed656e56903ab500905334fb87beea7")]
public partial class WaitCoolTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Monster;
    [SerializeReference] public BlackboardVariable<float> CoolTime;
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    private float elapsedTime;
    protected override Status OnStart()
    {
        elapsedTime = 0;
        Animator.Value.SetTrigger("Idle");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= CoolTime.Value)
        {
            return Status.Success;
        }

        return Status.Running;
    }
    
}

