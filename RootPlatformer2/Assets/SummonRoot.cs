using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRoot : MonoBehaviour
{
    public GameObject rootObject;
    private GameObject currentRoot;
    private Transform currentStick;
    private Transform currentRootPoint;
    public float growthRate;
    private Transform lameRootParent;
    private Vector3 lameRootLocalPosition;
    private Quaternion lameStartRotation;
    private Vector3 lameDown;

    [SerializeField] private ParticleSystem grassParticles;
    // Start is called before the first frame update
    void Start()
    {
        grassParticles.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentRoot)
        {
            UpdateLameParent();
        }
        if (Input.GetKeyDown(KeyCode.C) && currentRoot)
        {
            GetRidOfRoot();
            return;
        }


        if(Input.GetKeyDown(KeyCode.X) && currentRoot == null) {
            MakeNewRoot();
            return;
        }
        if (!Input.GetKey(KeyCode.X)) return;
        Vector3 stickScale = currentStick.localScale;
        currentStick.localScale = new Vector3(stickScale.x, stickScale.y + growthRate * Time.deltaTime, stickScale.z);
        currentStick.position += Vector3.down * growthRate * Time.deltaTime / 2;
        currentRootPoint.position += Vector3.down * growthRate * Time.deltaTime;
        
        //currentRoot.transform.rotation = Quaternion.FromToRotation();
    }
    void MakeNewRoot()
    {
        bool hitSomething = Physics.Raycast(transform.position, Vector2.up, out RaycastHit hitInfo, float.PositiveInfinity, ~LayerMask.NameToLayer("Default"));
        if (!hitSomething) return;
        //if (hitInfo.transform.rotation != Quaternion.identity) return;
        currentRoot = Instantiate(rootObject, hitInfo.point, Quaternion.identity);

        //currentRoot = Instantiate(rootObject, hitInfo.point, Quaternion.identity, hitInfo.transform);

        SetupLameParent(hitInfo);
        grassParticles.Play();
        //currentRoot.transform.SetParent(hitInfo.transform, true);
        //currentRoot.transform.parent = hitInfo.transform;
        //currentRoot.transform.localScale = hitInfo.transform.InverseTransformVector(Vector3.one);
        //currentRoot.transform.rotation = Quaternion.identity;
        //
        currentRoot.transform.localScale = new Vector3(1 / currentRoot.transform.lossyScale.x, 1 / currentRoot.transform.lossyScale.y, 1/ currentRoot.transform.lossyScale.z);
        currentStick = currentRoot.transform.GetChild(0);
        currentRootPoint = currentRoot.transform.GetChild(1);

    }
    void SetupLameParent(RaycastHit rh)
    {
        lameRootParent = rh.transform;
        lameRootLocalPosition = lameRootParent.InverseTransformPoint(rh.point);
        //lameStartRotation = lameRootParent.rotation;
        //lameDown = lameRootParent.InverseTransformDirection(Vector3.down);
    }
    void UpdateLameParent()
    {
        currentRoot.transform.position = lameRootParent.TransformPoint(lameRootLocalPosition);
        //currentRoot.transform.rotation = Quaternion.FromToRotation(lameStartRotation, lameRootParent.rotation);
    }
    void GetRidOfRoot()
    {
        grassParticles.Stop();
        Destroy(currentRoot);
        currentRoot = null;
        currentStick = null;
        currentRootPoint = null;
    }
}
