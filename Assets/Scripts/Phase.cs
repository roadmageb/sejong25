using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhaseInfo
{
    public static int RateArrangePoint(PhaseEnum phase)
    {
        switch (phase)
        {
            case PhaseEnum.Start: return 300;
            case PhaseEnum.Main: return 150;
            case PhaseEnum.Finale: return 50;
            default: return 0;
        }
    }
    public static float GradeProb(PhaseEnum phase, int gradeProbIndex)
    {
        float gradeProb = 0;
        switch (phase)
        {
            case PhaseEnum.Start:
                switch (gradeProbIndex)
                {
                    case 0: gradeProb = 0.4f; break;
                    case 1: gradeProb = 0.8f; break;
                    case 2: gradeProb = 1; break;
                }
                break;
            case PhaseEnum.Main:
                switch (gradeProbIndex)
                {
                    case 0: gradeProb = 0.5f - 0.5f * WordSpace.inst.playerTypingRate; break;
                    case 1: gradeProb = 1 - 0.5f * WordSpace.inst.playerTypingRate; break;
                    case 2: gradeProb = 1 - 0.15f * WordSpace.inst.playerTypingRate; break;
                }
                break;
            case PhaseEnum.Finale:
                switch (gradeProbIndex)
                {
                    case 0: gradeProb = 0.2f - 0.2f * WordSpace.inst.playerTypingRate; break;
                    case 1: gradeProb = 0.8f - 0.45f * WordSpace.inst.playerTypingRate; break;
                    case 2: gradeProb = 0.9f - 0.15f * WordSpace.inst.playerTypingRate; break;
                }
                break;
        }
        return gradeProb;
    }
}