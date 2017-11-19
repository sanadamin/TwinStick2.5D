using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyReplay : MonoBehaviour
{

    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrame = new MyKeyFrame[bufferFrames];

    private Rigidbody rigidbody;
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameManager.FindObjectOfType<GameManager>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameManager.recording == true)
        {
            Record();
        }
        else if(gameManager.recording == false)
        {
            PlayBack();
        }
    }
    void Record()
    {
        rigidbody.isKinematic = false;
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        Debug.Log("Writing Frame: " + frame);

        keyFrame[frame] = new MyKeyFrame(time, transform.position, transform.rotation);
    }
    void PlayBack()
    {
        rigidbody.isKinematic = true;
        int frame = Time.frameCount % bufferFrames;

        Debug.Log("Reading Frame: " + frame);
        transform.position = keyFrame[frame].position;
        transform.rotation = keyFrame[frame].rotation;
    }

}
/// <summary>
/// A Struct for storing position, rotation and fframtetime;
/// </summary>
public struct MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float aTime, Vector3 aPosition, Quaternion aRotation)
    {
        frameTime = aTime;
        position = aPosition;
        rotation = aRotation;
    }
}
