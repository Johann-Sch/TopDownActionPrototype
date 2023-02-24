using UnityEngine;

[CreateAssetMenu(menuName = "CooldownedSkill/ImprovedAttackData")]

public class ImprovedAttackData : AttackData
{
    [field: SerializeField] public Color ColliderColor { get; protected set; } = Color.white;
    [field: SerializeField] public PrimitiveType PrimitiveType { get; protected set; } = PrimitiveType.Sphere;
    [field: SerializeField] public Mesh Mesh { get; protected set; }
    [field: SerializeField] public Vector3 Scale { get; protected set; } = Vector3.one;

    public override void AttackBehaviour(ComboDelay comboDelay)
    {
        base.AttackBehaviour(comboDelay);
        
        AttackCollider attackCollider = comboDelay.Character.AttackCollider;

        CreateCollider(attackCollider);

        attackCollider.GetComponent<Renderer>().material.color = ColliderColor;
        attackCollider.GetComponent<MeshFilter>().mesh = Mesh;
        attackCollider.transform.localScale = Scale;
        attackCollider.gameObject.SetActive(true);
    }

    private void CreateCollider(AttackCollider attackCollider)
    {
        Collider collider;
        
        switch (PrimitiveType)
        {
            case PrimitiveType.Cube:
                collider = attackCollider.gameObject.AddComponent<BoxCollider>();
                break;
            case PrimitiveType.Sphere:
                collider = attackCollider.gameObject.AddComponent<SphereCollider>();
                break;
            case PrimitiveType.Cylinder:
            case PrimitiveType.Capsule:
                collider = attackCollider.gameObject.AddComponent<CapsuleCollider>();
                break;
            default:
                return;
                break;
        }

        collider.isTrigger = true;
    }
}
