using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class ConvertAnimationToStepped : MonoBehaviour
{
    [MenuItem("Project Tools/ConvertAnimationsToStepped")]
    public static void ConvertAnimationsToStepped()
    {
        AnimationClip[] animationClips = AnimationUtility.GetAnimationClips(Selection.activeGameObject);
        if (animationClips.Length > 0)
        {

            foreach (AnimationClip animationClip in animationClips)
            {

                EditorCurveBinding[] CurveBindings = AnimationUtility.GetCurveBindings(animationClip);
                for (int i = 0; i < CurveBindings.Length; i++)
                {
                    ConvertRotationInterpolationCurvesToEulerAngles(CurveBindings[i], animationClip, "x");
                    ConvertRotationInterpolationCurvesToEulerAngles(CurveBindings[i], animationClip, "y");
                    ConvertRotationInterpolationCurvesToEulerAngles(CurveBindings[i], animationClip, "z");

                }



            }
        }
    }

        static void ConvertRotationInterpolationCurvesToEulerAngles(EditorCurveBinding curveBinding, AnimationClip clip, string dimensionLetter)
        {
            if (curveBinding.propertyName == "m_LocalRotation." + dimensionLetter)
            {
                EditorCurveBinding newCurveBinging = new EditorCurveBinding();
                newCurveBinging = curveBinding;
                newCurveBinging.propertyName = "localEulerAnglesRaw." + dimensionLetter;
                AnimationCurve curve = new AnimationCurve();
                curve = AnimationUtility.GetEditorCurve(clip, curveBinding);
                AnimationUtility.SetEditorCurve(clip, newCurveBinging, curve);

                //This line deletes both
                //AnimationUtility.SetEditorCurve(clip, curveBinding, null);

        }
        }

    }

  

    

/*

    public static EditorCurveBinding[] ConvertToEuler(EditorCurveBinding[] selection)
    {
        string prefix = "localEulerAnglesBaked";
        EditorCurveBinding[] newCurves = new EditorCurveBinding[3];
        newCurves[0] = selection[0];
        newCurves[1] = selection[1];
        newCurves[2] = selection[2];

        newCurves[0].propertyName = prefix + ".x";
        newCurves[1].propertyName = prefix + ".y";
        newCurves[2].propertyName = prefix + ".z";

        return newCurves;
    }

    /*

    internal static EditorCurveBinding[] ConvertRotationPropertiesToInterpolationType(EditorCurveBinding[] selection, Mode newInterpolationMode)
    {
        if (selection.Length != 4)
            return selection;

        if (GetModeFromCurveData(selection[0]) == Mode.RawQuaternions)
        {
            EditorCurveBinding[] newCurves = new EditorCurveBinding[3];
            newCurves[0] = selection[0];
            newCurves[1] = selection[1];
            newCurves[2] = selection[2];

            string prefix = GetPrefixForInterpolation(newInterpolationMode);
            newCurves[0].propertyName = prefix + ".x";
            newCurves[1].propertyName = prefix + ".y";
            newCurves[2].propertyName = prefix + ".z";

            return newCurves;
        }
        else
            return selection;
    }
    */

        /*
    public static string GetPrefixForInterpolation(Mode newInterpolationMode)
    {
        if (newInterpolationMode == Mode.Baked)
            return "localEulerAnglesBaked";
        else if (newInterpolationMode == Mode.NonBaked)
            return "localEulerAngles";
        else if (newInterpolationMode == Mode.RawEuler)
            return "localEulerAnglesRaw";
        else if (newInterpolationMode == Mode.RawQuaternions)
            return "m_LocalRotation";
        else
            return null;
    }
    */
    

