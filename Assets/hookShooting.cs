using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookShooting : MonoBehaviour
{
    public float range = 10000;
    public float hookAcceleration = 100;
    public float hookSpeed = 50;
    public Camera cam;
    public bool shootDisabled = false;
    public GameObject hook;
    public GameObject hook2;
    public LayerMask ground;
    public bool pullPlayer = false;
    public bool pullObject = false;
    public float maxPullTime = 3f;
    public float currPullTime = 0;
    public float offset = 0.3f;
    public playerMove pm;
    public CapsuleCollider playerCapsule;

    public float ropeSegmentLength = 0.8f;
    public float hookForce = 100f;
    public Transform ropeSegments;
    public GameObject ropePrefab;
    public GameObject gun;
    public GameObject ropeStuff;
    public GameObject player;
    float pullClipDist = 1f;
    float releaseRad = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!shootDisabled && !pullPlayer && !pullObject) Shoot();
            shootDisabled = true;
        }
        else shootDisabled = false;
        if (pullPlayer)
        {
            cam.nearClipPlane = pullClipDist;
            hook.GetComponent<MeshRenderer>().enabled = true;
            hook2.GetComponent<MeshRenderer>().enabled = false;
            currPullTime += Time.smoothDeltaTime;
            if ((hook.transform.position - playerCapsule.ClosestPoint(hook.transform.position)).magnitude < releaseRad || currPullTime > maxPullTime)
            {
                pullPlayer = false;
                pm.velocity = Vector3.zero;
            }
            else if (Input.GetKey("space") && currPullTime > 0.5f)
            {
                jumpSound.Play();
                pullPlayer = false;
                float realeaseModifier = 0.8f;
                pm.velocity = new Vector3(pm.velocity.x * realeaseModifier, pm.jumpVelocity+ pm.velocity.y * realeaseModifier, pm.velocity.z * realeaseModifier);
            }
            else
            {
                Quaternion desiredDir = Quaternion.LookRotation(hook.transform.position - playerCapsule.ClosestPoint(hook.transform.position));
                Quaternion currDir = Quaternion.LookRotation(pm.velocity);
                float dif = Quaternion.Angle(desiredDir, currDir);
                float magnitude = pm.velocity.magnitude * Mathf.Cos(Mathf.PI / 180f * dif);
                magnitude += hookAcceleration * Time.smoothDeltaTime;
                pm.velocity = (hook.transform.position - playerCapsule.ClosestPoint(hook.transform.position)).normalized * magnitude;
            }
            doRopes();
        }
        else if (pullObject)
        {
            cam.nearClipPlane = pullClipDist;
            hook.GetComponent<MeshRenderer>().enabled = true;
            hook2.GetComponent<MeshRenderer>().enabled = false;
            currPullTime += Time.smoothDeltaTime;
            if ((hook.transform.position - playerCapsule.ClosestPoint(hook.transform.position)).magnitude < releaseRad || currPullTime > maxPullTime)
            {
                pullObject = false;
            }
            else
            {
                hook.transform.position += (playerCapsule.ClosestPoint(hook.transform.position) - hook.transform.position).normalized * hookSpeed * Time.smoothDeltaTime;
            }
            doRopes();
        }
        else
        {
            pullSound.Pause();
            cam.nearClipPlane = 0.01f;
            int numToDestroy = ropeSegments.childCount;
            for (int i = 0; i < numToDestroy; i++)
            {
                Destroy(ropeSegments.GetChild(i).gameObject);
            }
            gun.transform.localPosition = new Vector3(2.12f, -1.01f, 3.69f);
            gun.transform.localRotation = Quaternion.Euler(0, 90, 0);
            gun.GetComponent<MeshRenderer>().enabled = true;
            currPullTime = 0;
            hook2.GetComponent<MeshRenderer>().enabled = true;
            hook.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, ground))
        {
            pullSound.Play();
            hook.transform.position = hit.point + hit.normal * offset;
            Quaternion rot = Quaternion.LookRotation(hit.normal);
            if (hit.collider.gameObject.layer == 10)
            {
                grabSound.Play();
                hook.transform.rotation = Quaternion.LookRotation(playerCapsule.ClosestPoint(hit.point) - hit.point);
                hook.transform.Rotate(Vector3.up * -90);
                pullObject = true;
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce((playerCapsule.ClosestPoint(hit.point) - hit.point).normalized * hookForce* hit.distance);
                hookSpeed = Mathf.Min(3 * hit.distance,100);

            }
            else
            {
                pullStartSound.Play();
                hook.transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y - 90, -rot.eulerAngles.x);
                pullPlayer = true;
            }
        }
    }
    public int numRopeSegments;
    public void doRopes()
    {

        //gun.transform.localPosition = new Vector3(-0.2f, -2.4f, 3.69f);
        //gun.transform.rotation = Quaternion.LookRotation(hook.transform.position - player.transform.position);
        //gun.transform.Rotate(0, 90, 0);
        /*if (Mathf.Abs((gun.transform.rotation.eulerAngles.y+360)%360 - (player.transform.rotation.eulerAngles.y+360)%360) > 90 && Mathf.Abs(gun.transform.rotation.eulerAngles.y % 360 - player.transform.rotation.eulerAngles.y % 360) < 270)*/ gun.GetComponent<MeshRenderer>().enabled = false;
        numRopeSegments = (int)Mathf.Ceil((hook.transform.position - player.transform.position).magnitude / ropeSegmentLength);
        if (ropeSegments.childCount > numRopeSegments)
        {
            int deletedRopes = ropeSegments.childCount - numRopeSegments;
            for (int i = 0; i < deletedRopes; i++)
            {
                Destroy(ropeSegments.GetChild(i).gameObject);
            }
        } else if (ropeSegments.childCount < numRopeSegments)
        {
            int numRopesNeeded = numRopeSegments - ropeSegments.childCount;
            for (int i = 0; i < numRopesNeeded; i++)
            {
                Instantiate(ropePrefab, ropeSegments);
            }
        }
        ropeStuff.transform.rotation = Quaternion.LookRotation(hook.transform.position - player.transform.position);
        for (int i = 0; i < ropeSegments.childCount; i++)
        {
            ropeSegments.GetChild(i).localPosition = new Vector3(0, 0, i * ropeSegmentLength);
            ropeSegments.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }

        //if (ropeSegments.childCount > numRopeSegments)
        //{
        //    while (ropeSegments.childCount > numRopeSegments)
        //        Destroy(ropeSegments.GetChild(numRopeSegments));
        //} else if(ropeSegments.childCount < numRopeSegments)
        //{
        //    int numRopesNeeded = numRopeSegments - ropeSegments.childCount;
        //    for(int i = 0; i<numRopesNeeded; i++)
        //    {
        //        Instantiate(ropePrefab, ropeSegments);
        //    }
        //}
        //for(int i = 0; i<ropeSegments.childCount; i++)
        //{
        //    ropeSegments.GetChild(i).localPosition = new Vector3(i * ropeSegmentLength, 0, 0);
        //    ropeSegments.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        //}
    }
    public AudioSource pullSound;
    public AudioSource grabSound;
    public AudioSource jumpSound;
    public AudioSource pullStartSound;
    void Start()
    {

        //pullSound.Play();
        //pullSound.Pause();
    }
}
