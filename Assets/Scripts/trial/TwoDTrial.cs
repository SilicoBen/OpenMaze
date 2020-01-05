﻿using System;
using data;
using UnityEngine;

namespace trial
{
    //This is a two dimensional trial
    public class TwoDTrial : TimeoutableTrial
    {
        public TwoDTrial(int blockId, int trialId) : base(blockId, trialId)
        {
        }

        public override void PreEntry(TrialProgress t, bool first = true)
        {

            base.PreEntry(t, first);
            t.EnvironmentType = trialData.Scene;
            t.CurrentEnclosureIndex = trialData.Enclosure - 1;
            t.BlockID = BlockID;
            t.TrialID = TrialID;
            t.TwoDim = trialData.TwoDimensional;
            t.Instructional = 0;
            t.LastX = t.TargetX;
            t.LastY = t.TargetY;
            t.TargetX = 0;
            t.TargetY = 0;

            LoadNextSceneWithTimer(trialData.Scene);
        }

        //Code for a trial to continue
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var trialEndKeyCode = trialData.TrialEndKey;
            var ignoreUserInputDelay = DataSingleton.GetData().IgnoreUserInputDelay;

            if (!String.IsNullOrEmpty(trialEndKeyCode) && Input.GetKey(trialEndKeyCode.ToLower()) && (_runningTime > ignoreUserInputDelay))
            {
                Debug.Log(_runningTime);
                Progress();
            }
        }
    }
}