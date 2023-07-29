using UnityEngine;

 class Rotator : MonoBehaviour
{
    [SerializeField] float angularSpeed;
    [SerializeField] Space space;
    [SerializeField] Vector3 axis = Vector3.up;

    void Update()
    {
        transform.Rotate(axis, angularSpeed * Time.deltaTime, space);
    }
   
}
