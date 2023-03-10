using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHaptic : MonoBehaviour
{
    public static MyHaptic instance;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator CreateVibrateTime(int iteration, int frequency, int strength, OVRInput.Controller controller, float time)
    {
        var channel = controller == OVRInput.Controller.LTouch ? OVRHaptics.LeftChannel : OVRHaptics.RightChannel;

        byte[] sample = new byte[iteration];

        for (int i = 0; i < iteration; i++)
        {
            sample[i] = i % frequency == 0 ? (byte)0 : (byte)strength;
        }

        OVRHapticsClip createdClip = new OVRHapticsClip(sample, sample.Length);

        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            Debug.Log("Play vib");
            channel.Queue(createdClip);
        }
        yield return new WaitForSeconds(time);
        channel.Clear();
        yield return null;
    }

    public void PlayVibration(bool rightHanded, int force, float time)
    {
        if (rightHanded) StartCoroutine(CreateVibrateTime(64, 100, force, OVRInput.Controller.RTouch, time));
        else StartCoroutine(CreateVibrateTime(64, 100, force, OVRInput.Controller.LTouch, time));
    }
}
