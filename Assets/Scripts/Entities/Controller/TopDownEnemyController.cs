using UnityEngine;

public class TopDownEnemyController : TopDownController
{
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        ClosestTarget = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()
    {
        if (ClosestTarget == null) return 0;
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        if (ClosestTarget == null) return Vector2.zero;
        return (ClosestTarget.position - transform.position).normalized;
    }
}
