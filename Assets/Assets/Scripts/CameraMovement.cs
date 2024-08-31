using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    Volume volume;
    GameObject player;
    [SerializeField] float damp = .1f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        volume = Camera.main.GetComponent<Volume>();

        volume.enabled = intToBool(PlayerPrefs.GetInt("Volume", 1));
    }
    void Update()
    {
        if(player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 camPos = Vector3.Lerp(transform.position, player.transform.position, damp * Time.deltaTime);
        camPos.y = Mathf.Clamp(camPos.y, 0f, Mathf.Infinity);
        camPos.z = -10f;
        transform.position = camPos;
    }

    bool intToBool(int integer)
    {
        if (integer == 0)
        {
            return false;
        }
        else if (integer == 1)
        {
            return true;
        }
        else
        {
            Debug.LogError("Parameter doesn't contain 0 or 1: " + integer);
            return false;
        }
    }

}
