using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    Transform currentTarget;
    Animator anim;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform enemyTarget_Locator;

    [Tooltip("StateDrivenMethod for Switching Cameras")]

    [SerializeField] Animator cinemachineAnimator;
    [Header("Settings")]
    [SerializeField] bool zeroVert_look;
    [SerializeField] float noticeZone = 10;
    [SerializeField] float lookAtSmoothing = 2;
    [Tooltip("Angle_Degree")] [SerializeField] float maxNoticeAngle = 60;
    [SerializeField] float crossHair_Scale = 0.1f;

    Transform cam;
    bool enemyLocked;
    float currentYOffset;
    Vector3 pos;

    [SerializeField] CameraFollow camFollow;
    [SerializeField] Transform lockOnCanva;
    Isometriccontroller movement;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Isometriccontroller>();
        anim = GetComponent<Animator>();
        cam = Camera.main.transform;
        lockOnCanva.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        camFollow.lockedTarget = enemyLocked;
        if(InputGetKeyDown(KeyCode.Q))
        {
            if(currentTarget)
            {
                ResetTarget();
                return;
            }

            if(currentTarget = ScanNearBy()) FoundTarget(); else ResetTarget();
        }

        if(enemyLocked)
        {
            if(!TargetOnRange()) ResetTarget();
            LookAtTarget();
        }
    }

    void FoundTarget()
    {
        movement.lockMovement = true;
        lockOnCanva.gameObject.SetActive(true);
        anim.SetLayerWeight(1, 1);
        cinemachineAnimator.Play("TargetCamera");
        enemyLocked = true;
    }

    void ResetTarget()
    {
        movement.lockMovement = false;
        lockOnCanva.gameObject.SetActive(false);
        currentTarget = null;
        anim.SetLayerWeight(1, 0);
        cinemachineAnimator.Play("FollowCamera");
        enemyLocked = false;
    }

    private Transform ScanNearBy()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(transform.position, noticeZone, targetLayer);
        float closestAngle = maxNoticeAngle;
        Transform closestTarget = null;
        if(nearbyTargets.Lenght <= 0) return null;

        for (int i = 0; i < nearbyTargets.Length; i++)
        {
            Vector3 dir = nearbyTargets[i].transform.position - cam.position;
            dir.y = 0;
            float _angle = Vector3.Angle(cam.forward, dir);

            if(_angle < closestAngle)
            {
                closestTarget = nearbyTargets[i].transform;
                closestAngle = _angle;
            }
        }

        if(!closestTarget) return null;
        float h1 = closestTarget.GetComponent<CapsuleCollider>().height;
        float h2 = closestTarget.localScale.y;
        float h = h1 * h2;
        float half_h = (h / 2) / 2;

        currentYOffset = h - half_h;
        if(zeroVert_look && currentYOffset > 1.6f && currentYOffset < 1.6f * 3) currentYOffset = 1.6f;
        Vector3 tarPos = closestTarget.position + new Vector3(0, currentYOffset, 0);
        if(Blocked(tarPos)) return null;
        return closestTarget;
    }

    bool Blocked()
    {
        RaycastHit hit;
        if()
    }
}
