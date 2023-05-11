using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    [SerializeField] private CameraMovement cam;
    public bool moveCamera;

    public Collider2D enemyRoom;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (moveCamera)
            {
                cam.minPosition += cameraChange;
                cam.maxPosition += cameraChange;
            }
            other.transform.position = playerChange;
            if (enemyRoom != null)
            {
                enemyRoom.enabled = true;
            }
        }
    }
}
