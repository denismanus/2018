using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    private GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    public GameObject background;
    // Use this for initialization
    void Start()
    {
        FindCharacter();
    }

    public void FindCharacter()
    {
        target = FindObjectOfType<TestScript>().gameObject;
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 15f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
        background.GetComponent<Transform>().position = new Vector3(
            GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 2);
    }
}