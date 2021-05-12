using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private float moveDuration = 0.45f;

    [SerializeField, Range(0f, 100f)]
    private float collideBreakForce = 40f;

    [SerializeField]
    private bool useGravityOnPickUp = false;

    private Rigidbody rb;
    private bool hasOwner;

    public int Layer => gameObject.layer;
    public Rigidbody Rigidbody => rb;
    

    public static event Action onForceRelease;
    public static event Action onResetState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void SetPickedUp()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = useGravityOnPickUp;
        hasOwner = true;
    }

    public void ResetState(Transform newTransform)
    {
        ReleaseSoft();
        gameObject.SetActive(true);
        transform.position = newTransform.position;
        onResetState?.Invoke();
    }

    public void SlideToPos(Transform position)
    {
        rb.isKinematic = true;
        StartCoroutine(MoveToPos(position));
    }

    public void ApplyKick(Vector3 point, Vector3 dir, float kickForce)
    {
        //rb.AddForce(dir * kickForce, ForceMode.Impulse);
        rb.AddForceAtPosition(dir * kickForce, point, ForceMode.Impulse);
    }

    IEnumerator MoveToPos(Transform position)
    {
        float interpolator = 0f;
        var oldPos = transform.position;

        while (interpolator <= 1f)
        {
            transform.position = Vector3.Lerp(oldPos, position.position, interpolator);
            interpolator += Time.deltaTime / moveDuration;
            yield return null;
        }

        SetParent(position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (hasOwner)
        {
            if (collision.relativeVelocity.sqrMagnitude > collideBreakForce)
            {
                onForceRelease?.Invoke();
            }
        }
    }

    private void SetParent(Transform position)
    {
        transform.SetParent(position);
    }

    public void ReleaseParent()
    {
        transform.SetParent(null);
    }

    public void Release()
    {
        rb.isKinematic = false;
        ReleaseParent();
    }

    public void ReleaseSoft()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        hasOwner = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
