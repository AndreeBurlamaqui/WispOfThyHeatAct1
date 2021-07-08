using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    [SerializeField] private float lowIntensity, normalIntensity, highIntesity;
    [SerializeField] private float lowRumbleIntensity, normalRumbleIntensity, highRumbleIntesity;

    [SerializeField] private AnimationCurve shakeRumbleCurve;
    [SerializeField] private CinemachineVirtualCamera actualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin shakeRumbleCamera;
    [SerializeField] private CinemachineTargetGroup groupCamera;

    private void Awake() => Instance = this;
    public enum ShakeRumbleIntensity {Low, Normal, High}
    public CinemachineVirtualCamera CurrentCamera
    {
        get => actualCamera;
        set
        {
            actualCamera = value;
            shakeRumbleCamera = actualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            groupCamera = actualCamera.transform.parent.GetComponentInChildren<CinemachineTargetGroup>();
            groupCamera.m_Targets[0].target = PlayerManager.Instance.GetPlayerGO.transform;
            StopGroupCam();
        }
    }
    public IEnumerator ShakeRumble(ShakeRumbleIntensity intensity, float duration, bool isShakeOnly)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey += Time.unscaledDeltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            float curvePercent = shakeRumbleCurve.Evaluate(percent);

            switch (intensity)
            {
                case ShakeRumbleIntensity.Low:

                    shakeRumbleCamera.m_AmplitudeGain = Mathf.Lerp(lowIntensity, 0, curvePercent);

                    if (!isShakeOnly)
                    {
                        if (PlayerManager.Instance.WhichDeviceIsBeingUsed == PlayerManager.ControlDeviceType.Gamepad)
                            Gamepad.current.SetMotorSpeeds(Mathf.Lerp(lowRumbleIntensity / 1.5f, 0, curvePercent), Mathf.Lerp(lowRumbleIntensity, 0, curvePercent));
                    }

                    break;

                case ShakeRumbleIntensity.Normal:

                    shakeRumbleCamera.m_AmplitudeGain = Mathf.Lerp(normalIntensity, 0, curvePercent);

                    if (!isShakeOnly)
                    { 
                        if (PlayerManager.Instance.WhichDeviceIsBeingUsed == PlayerManager.ControlDeviceType.Gamepad)
                            Gamepad.current.SetMotorSpeeds(Mathf.Lerp(normalRumbleIntensity / 1.5f, 0, curvePercent), Mathf.Lerp(normalRumbleIntensity, 0, curvePercent));
                    }

                    break;

                case ShakeRumbleIntensity.High:

                    shakeRumbleCamera.m_AmplitudeGain = Mathf.Lerp(highIntesity, 0, curvePercent);

                    if (!isShakeOnly)
                    {
                        if (PlayerManager.Instance.WhichDeviceIsBeingUsed == PlayerManager.ControlDeviceType.Gamepad)
                            Gamepad.current.SetMotorSpeeds(Mathf.Lerp(highRumbleIntesity / 1.5f, 0, curvePercent), Mathf.Lerp(highRumbleIntesity, 0, curvePercent));
                    }

                    break;

                default: 

                    Debug.Log("Out of range intensity shake/rumble, going Low");
                    shakeRumbleCamera.m_AmplitudeGain = Mathf.Lerp(lowIntensity, 0, curvePercent);

                    if (!isShakeOnly)
                    {
                        if (PlayerManager.Instance.WhichDeviceIsBeingUsed == PlayerManager.ControlDeviceType.Gamepad)
                            Gamepad.current.SetMotorSpeeds(Mathf.Lerp(lowRumbleIntensity / 1.5f, 0, curvePercent), Mathf.Lerp(lowRumbleIntensity, 0, curvePercent));
                    }

                    break;
            }

            yield return null;

        }

        StopShakeRumble();

    }
    private void StopShakeRumble()
    {
        shakeRumbleCamera.m_AmplitudeGain = 0f;

        if (PlayerManager.Instance.WhichDeviceIsBeingUsed == PlayerManager.ControlDeviceType.Gamepad)
            Gamepad.current.SetMotorSpeeds(0, 0);
    }

    public void ChangeToGroupCam(Transform otherGOToGroup)
    {
        groupCamera.m_Targets[1].target = otherGOToGroup;
        groupCamera.gameObject.SetActive(true);
        actualCamera.Follow = groupCamera.transform;
    }

    public void StopGroupCam()
    {
        actualCamera.Follow = PlayerManager.Instance.GetPlayerGO.transform;
        groupCamera.m_Targets[1].target = null;
        groupCamera.gameObject.SetActive(false);
    }
}
