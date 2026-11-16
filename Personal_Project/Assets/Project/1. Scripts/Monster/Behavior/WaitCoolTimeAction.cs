using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitCoolTime", story: "[Monster] stop until [CoolTime]", category: "Action", id: "7ed656e56903ab500905334fb87beea7")]
public partial class WaitCoolTimeAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Monster;
    [SerializeReference] public BlackboardVariable<float> CoolTime;
    
    private float elapsedTime;
    protected override Status OnStart()
    {
        elapsedTime = 0;
        
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

