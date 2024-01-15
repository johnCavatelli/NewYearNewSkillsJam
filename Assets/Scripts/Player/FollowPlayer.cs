using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform camPosition;

    private void LateUpdate() {
        transform.position = camPosition.position;
    }
}
