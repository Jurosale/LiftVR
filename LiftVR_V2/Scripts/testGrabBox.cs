using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGrabBox : genericGrabObject {

    private Vector3 offset;

    public override void onGrab(GameObject grabber)
    {
        offset = new Vector3(grabber.transform.position.x - transform.localPosition.x, grabber.transform.position.y - transform.localPosition.y, grabber.transform.position.z - transform.localPosition.z);
    }

    public override void whileGrabbed(GameObject grabber)
    {
        transform.localPosition = grabber.transform.position + offset;
    }

    public override void onRelease(GameObject grabber)
    {

    }
}
