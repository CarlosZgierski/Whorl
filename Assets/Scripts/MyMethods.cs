using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMethods
{
    public static float ConvertValueFromOldScaleToNewScale(float valueInOldScale, float minValueInOldScale, float maxValueInOldScale, float minValueInNewScale, float maxValueInNewScale)
    {
        float valueInNewScale;

        valueInNewScale = ((valueInOldScale - minValueInOldScale) * (maxValueInNewScale - minValueInNewScale)) / (maxValueInOldScale - minValueInOldScale) + minValueInNewScale;
        return valueInNewScale;
    }
}