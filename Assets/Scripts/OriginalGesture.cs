using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

public class OriginalGesture : MonoBehaviour
{

    [SerializeField, Range(0.0f, 90.0f)] private float flatHandThreshold = 45.0f;
    [SerializeField, Range(0.0f, 90.0f)] private float facingThreshold = 80.0f;
    [SerializeField] GameObject sphere = null;
    [SerializeField] TextMesh textMesh = null;
    private bool handSign = false;


    private void Update()
    {
        if (CheckHandSign())
        {
            handSign = true;
        }

        if (!sphere.activeSelf & !CheckHandSign() & handSign)
        {
            sphere.SetActive(true);
            handSign = false;
        }
        else if (sphere.activeSelf & !CheckHandSign() & handSign)
        {
            sphere.SetActive(false);
            handSign = false;
        }

    }

    private bool CheckHandSign()
    {
        // 右手のみ判定したいので右手を取得
        var jointedHand = HandJointUtils.FindHand(Handedness.Right);

        // 手のひらが認識できているか
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose palmPose))
        {
            MixedRealityPose thumbTipPose, indexTipPose, middleTipPose, ringTipPose, pinkyTipPose;

            if (jointedHand.TryGetJoint(TrackedHandJoint.IndexTip, out indexTipPose) && jointedHand.TryGetJoint(TrackedHandJoint.PinkyTip, out pinkyTipPose))
            {
                var handNormal = Vector3.Cross(indexTipPose.Position - palmPose.Position, pinkyTipPose.Position - indexTipPose.Position).normalized;
                handNormal *= (jointedHand.ControllerHandedness == Handedness.Right) ? 1.0f : -1.0f;
                if (Vector3.Angle(palmPose.Up, handNormal) > flatHandThreshold)
                {
                    return false;
                }
            }

            // 中指と薬指の情報を取得
            if (jointedHand.TryGetJoint(TrackedHandJoint.MiddleTip, out middleTipPose) && jointedHand.TryGetJoint(TrackedHandJoint.RingTip, out ringTipPose))
            {
                var handNormal = Vector3.Cross(middleTipPose.Position - palmPose.Position, ringTipPose.Position - indexTipPose.Position).normalized;
                handNormal *= (jointedHand.ControllerHandedness == Handedness.Right) ? 1.0f : -1.0f;
                if (Vector3.Angle(palmPose.Up, handNormal) < flatHandThreshold)
                {
                    return false;
                }
            }
        }
        // 手のひらを向けているか確認
        return Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) < facingThreshold;
    }

}