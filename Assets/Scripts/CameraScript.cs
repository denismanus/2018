using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    private GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    public int z = 2;

    public Sprite[] sprites = new Sprite[3];

    //private float minX = 0, maxX = 25, minY = 0, maxY = 10;

    public GameObject background;
    // Use this for initialization
    void Start()
    {
        //FindCharacter();
    }

    public void ChangeBackground()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, 2)];
    }
    public void SetBoundaries(float minX, float maxX, float minY, float maxY)
    {
        //this.minX = minX + 2;
        //this.maxX = maxX;
        //this.minY = minY - 5;
        //this.maxY = maxY + 10;
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

            Vector3 boundaries = transform.position;
            //boundaries.x = Mathf.Clamp(boundaries.x, minX, maxX);
            //boundaries.y = Mathf.Clamp(boundaries.y, minY, maxY);
            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 15f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(boundaries, targetPos + offset, 0.25f);

        }
        background.GetComponent<Transform>().position = new Vector3( 
            GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, z);
    }
}