using System.Collections;
using UnityEngine;

public class RopeDrawer : MonoBehaviour
{
    /*
    [Header("Target Test")]
    [SerializeField] Transform target;
    bool targetFound;
    */

    [Header("Rope Curve Fields")]
    [SerializeField] float offsetStrength = 0f;
    [SerializeField] Transform ropeTransform = null;
    [SerializeField] Transform ropeDir = null;
    [SerializeField] AnimationCurve curve = null;

    //RopeStraightenSpeed
    [SerializeField] float straightenTimer = 0f;
    float timer = 0f;

    [Space(10)]
    //Moving rope field
    [SerializeField] float launchSpeed = 0f;
    [SerializeField] int curveResolution = 0;
    Vector3 ropeEnd;
    [HideInInspector] public bool reachingTarget;

    //Player's components
    PlayerMovement player;
    LineRenderer rope;
    AudioManager audioManager;
    Sound grappleSound;

    void Awake()
    {
        reachingTarget = false;
        player = GetComponent<PlayerMovement>();
        rope = GetComponent<LineRenderer>();
        grappleSound = FindObjectOfType<AudioManager>().GetClip("Grapple");
        //particle = FindObjectOfType<ParticleManager>();
    }

    ///*
    void LateUpdate()
    {
        //print("FOUND: " + targetFound + "       Reaching: " + reachingTarget);
        if (player.targetFound)
        {
            DrawStraightenRope(GetRopeWavePoints(player.target.position));
        }
        else if (!reachingTarget)
        {
            EraseRope();
        }

    }
    //*/


    public IEnumerator ShootRope()
    {
        timer = 0f;
        int repeat = 0;
        float ratio = 0f;
        float distance = Vector2.Distance(transform.position, player.target.position);
        float currentDistance = 0f;
        reachingTarget = true;
        ResetRopeTransform();
        grappleSound.Play();
        do
        {
            ropeEnd = Vector3.Lerp(transform.position, player.target.position, ratio);

            if (repeat > 0)
            {
                //To avoid <DividedByZeroException>
                DrawWaveRope(GetRopeWavePoints(ropeEnd));
            }

            currentDistance += launchSpeed * Time.fixedDeltaTime;
            ratio += currentDistance / distance;
            repeat++;
            yield return new WaitForFixedUpdate();
        } while (ratio < 1f);
        reachingTarget = false;
        player.targetFound = true;
        //particle.DashEffect(dashSpot);
    }

    void ResetRopeTransform()
    {
        ropeTransform.localPosition = Vector3.zero;

        Vector3 dir = player.target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ropeDir.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    Vector3[] GetRopeWavePoints(Vector3 target)
    {
        //Prepare rope points parameter
        Vector3[] ropePoints = new Vector3[curveResolution+1];

        float distance = Vector2.Distance(transform.position, target);
        float indexDistance = distance / curveResolution;
        float curveDistance = indexDistance / distance;

        for (int i = 0; i < curveResolution +1; i++)
        {
            //Just makes things clearer
            if (i == 0)
            {
                ropePoints[i] = transform.position;
                continue;
            }else if( i == curveResolution && player.targetFound)
            {
                ropePoints[i] = player.target.position;
                break;
            }

            //Creates offset
            float y = curve.Evaluate(curveDistance * i) * offsetStrength;
            float x = indexDistance * i ;

            //Creates desired offset direction
            ResetRopeTransform();
            ropeTransform.localPosition = new Vector2(x, y);
            ropePoints[i] = ropeTransform.position;

        }

        return ropePoints;
    }

    void DrawWaveRope(Vector3[] ropePoints)
    {
        rope.enabled = true;
        rope.positionCount = ropePoints.Length;

        //rope.SetPositions(ropePoints);

        for (int i = 0; i < ropePoints.Length; i++)
        {
            rope.SetPosition(i, ropePoints[i]);
        }

    }

    void DrawStraightenRope(Vector3[] ropePoints)
    {
        timer += Time.deltaTime;

        if (timer > straightenTimer)
        {
            timer = straightenTimer;
        }

        float ratio = timer / straightenTimer;

        for (int i = 0; i < ropePoints.Length; i++)
        {
            ResetRopeTransform();
            ropeTransform.position = ropePoints[i];
            ropeTransform.localPosition = new Vector3(ropeTransform.localPosition.x, 0f, 0f);

            Vector3 lerpPos = Vector3.Lerp(ropePoints[i], ropeTransform.position, ratio);

            rope.SetPosition(i, lerpPos);
        }
    }

    void EraseRope()
    {
        rope.enabled = false;
    }
}
