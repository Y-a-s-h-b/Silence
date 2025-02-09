using Destructible2D;
using Destructible2D.Examples;
using UnityEngine;
using static Destructible2D.D2dPolygonCollider;

[RequireComponent (typeof(D2dDestructibleSprite))]
[RequireComponent (typeof(D2dPolygonCollider))]
[RequireComponent (typeof(D2dSplitter))]
[RequireComponent (typeof(D2dSplitForce))]
[RequireComponent (typeof(Rigidbody2D))]
[ExecuteInEditMode]
public class DestructibleSprite : MonoBehaviour
{
    [Header("Split Force")]
    [SerializeField] private bool addForce = false;
    [SerializeField] private float force = 3;
    [SerializeField] private float forcePerSolidPixel = 0.0001f;

    [Header("Polygon Collider")]
    [SerializeField] private CellSizes cellSize = CellSizes.Square32;
    [SerializeField] private bool isTrigger = false;

    [Header("Splitter")]
    [SerializeField] private int feather = 16;

    private D2dSplitForce splitForce;
    private D2dPolygonCollider polygonCollider;
    private D2dSplitter splitter;
    private D2dDestructibleSprite destructibleSprite;

    private void Awake()
    {
        splitForce = GetComponent<D2dSplitForce>();
        polygonCollider = GetComponent<D2dPolygonCollider>();
        splitter = GetComponent<D2dSplitter>();
        destructibleSprite = GetComponent<D2dDestructibleSprite>();
    }

    private void Start()
    {
        SetupForce();
        splitter.Feather = feather;
    }

    private void SetupForce()
    {
        if (!addForce)
        {
            splitForce.Force = 0;
            return;
        }

        splitForce.ApplyTo = D2dDestructible.SplitMode.Split;
        splitForce.Force = force;
        splitForce.ForcePerSolidPixel = forcePerSolidPixel;
    }

    [ContextMenu("Rebuild")]
    public void Rebuild()
    {
        destructibleSprite.Rebuild();

        polygonCollider.CellSize = cellSize;
        polygonCollider.IsTrigger = isTrigger;
        polygonCollider.Rebuild();
    }
}
