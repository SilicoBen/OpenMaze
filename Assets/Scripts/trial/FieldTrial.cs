﻿using System;
using System.IO;
using main;
using SFB;
using UnityEngine;
using UnityEngine.UI;
using value;
using DS = data.DataSingleton;

namespace trial
{
    // This trial is loaded at the beginning. This is the Entry point of the application.
    public class FieldTrial : AbstractTrial
    {
        private readonly InputField[] _fields;
        private readonly ITrialService _trialService;

        // Here we construct the entire linked list structure.
        public FieldTrial(InputField[] fields) : base(null, BlockId.EMPTY, TrialId.EMPTY)
        {
            _fields = fields;
            TrialProgress = new TrialProgress();
            _fields = fields;
            _trialService = TrialService.Create();
            LoadingService = loading.LoadingService.Create();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (StartButton.clicked)
            {
                // Sets the output file name as the desired one.
                var subjectTextField = _fields[0].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.Subject = subjectTextField.text;

                var sessionTextField = _fields[1].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.SessionID = sessionTextField.text;

                var conditionTextField = _fields[2].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.Condition = conditionTextField.text;

                var noteTextField = _fields[3].transform.GetComponentsInChildren<Text>()[1];
                TrialProgress.Note = noteTextField.text;

                DS.GetData().OutputFile = TrialProgress.Subject + "_" +
                                          TrialProgress.SessionID + "_" +
                                          TrialProgress.Condition + "_" +
                                          TrialProgress.Note + "_" +
                                          DateTime.Now.ToString("yyyy-MM-dd-HH.mm.ss") + ".csv";

                _trialService.GenerateAllStartingTrials(this);

                Loader.LogHeaders();

                Progress();
            }
        }

        public override void Progress()
        {
            Loader.Get().CurrTrial = next;
            next.PreEntry(TrialProgress);
        }
    }
}
