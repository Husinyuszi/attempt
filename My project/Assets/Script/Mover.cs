using UnityEngine;

 class Mover : MonoBehaviour
{
    [SerializeField] Transform t1, t2;
    [SerializeField] Transform movable;
    [SerializeField] float speed = 2;
    [SerializeField, Range(0, 1)] float startPoint = 0.5f;

    Vector3 nextTarget;

    void OnValidate()
    {
        movable.position = Vector3.Lerp(t1.position, t2.position, startPoint);
    }
    void Start()
    {
        Vector3 p1 = t1.position;
        Vector3 p2 = t2.position;

        Vector3 p = Vector3.Lerp(p1, p2, startPoint);

        nextTarget = t2.position;
        movable.position = p;
    }

    void Update()
    {
        movable.position = Vector3.MoveTowards(movable.position, nextTarget, speed * Time.deltaTime);

        if (movable.position == nextTarget)
        {
            nextTarget = nextTarget == t1.position ? t2.position : t1.position;
        }
    }
}
