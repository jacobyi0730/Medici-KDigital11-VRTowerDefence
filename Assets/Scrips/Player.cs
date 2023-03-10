using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    private void Awake()
    {
        Player.instance = this;
    }

    public AnimationCurve animCurve;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            PlayShoot(false);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            PlayShoot(true);
        }
    }

    public IEnumerator CreateVibrateTime(int iteration, int frequency, int strength, OVRInput.Controller controller, float time)
    {
        var channel = controller == OVRInput.Controller.LTouch ? OVRHaptics.LeftChannel : OVRHaptics.RightChannel;

        byte[] sample = new byte[iteration];

        for (int i = 0; i < iteration; i++)
        {
            sample[i] = i % frequency == 0 ? (byte)0 : (byte)strength;
        }

        OVRHapticsClip  createdClip = new OVRHapticsClip(sample, sample.Length);

        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            Debug.Log("Play vib");
            channel.Queue(createdClip);
        }
        yield return new WaitForSeconds(time);
        channel.Clear();
        yield return null;
    }

    public void PlayShoot(bool rightHanded)
    {
        if (rightHanded) StartCoroutine(CreateVibrateTime(64, 100, 1000, OVRInput.Controller.RTouch, 1));
        else StartCoroutine(CreateVibrateTime(64, 100, 1000, OVRInput.Controller.LTouch, 1));
    }
}
