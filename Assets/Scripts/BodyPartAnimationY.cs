using UnityEngine;

public class BodyPartAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float height = 0.2f;
    [SerializeField] private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
  
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * height;
        transform.Rotate(0, 0, Time.deltaTime * 40);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
