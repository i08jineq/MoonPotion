﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DarkLordGame
{
    public class SelectMixingMethodScreen : MonoBehaviour
    {
        public MixingMethodButton mixingMethodButtonPrefab;
        public Transform buttonRoot;
        public Button closeButton;
        private MixingMethodType selectedMixingMethodType;

        private List<MixingMethodButton> mixingMethodButtons = new List<MixingMethodButton>();

        public Communicator<MixingMethodType> onChangedMixingMethod = new Communicator<MixingMethodType>();
        public Communicator onClosed = new Communicator();

        public void Setup()
        {
            List<MixingMethodType> mixingMethods = Singleton.instance.saveData.unlockedMixingMethodID;
            int count = mixingMethods.Count;
            MixingMethodButton mixingMethodButton = GameObject.Instantiate<MixingMethodButton>(mixingMethodButtonPrefab, buttonRoot);

            for (int i = 0; i < count; i++)
            {
                MixingMethodButton mixingMethodButtonClone = GameObject.Instantiate<MixingMethodButton>(mixingMethodButton, buttonRoot);
                mixingMethodButtonClone.Setup(mixingMethods[i]);
                mixingMethodButtonClone.onSelected.AddListener(OnSelectedMixingMethod);
                mixingMethodButtons.Add(mixingMethodButtonClone);
            }
            GameObject.Destroy(mixingMethodButton.gameObject);

            closeButton.onClick.AddListener(OnClickedClose);
        }

        public MixingMethodButton OnAddNewMixingMethod(MixingMethodType mixingMethodType)
        {
            MixingMethodButton mixingMethodButtonClone = GameObject.Instantiate<MixingMethodButton>(mixingMethodButtonPrefab, buttonRoot);
            mixingMethodButtonClone.Setup(mixingMethodType);
            mixingMethodButtonClone.onSelected.AddListener(OnSelectedMixingMethod);
            mixingMethodButtons.Add(mixingMethodButtonClone);
            return mixingMethodButtonClone;
        }

        private void OnSelectedMixingMethod(MixingMethodType mixingMethodType)
        {
            selectedMixingMethodType = mixingMethodType;
            int count = mixingMethodButtons.Count;
            for (int i = 0; i < count; i++)
            {
                if(mixingMethodType == mixingMethodButtons[i].mixingMethodType)
                {
                    continue;
                }
                mixingMethodButtons[i].SetUnSelected();
            }
            onChangedMixingMethod.Invoke(selectedMixingMethodType);
        }

        private void OnClickedClose()
        {
            gameObject.SetActive(false);
            onClosed.Invoke();
        }

        public void ResetUI()
        {
            selectedMixingMethodType = MixingMethodType.None;
            int count = mixingMethodButtons.Count;
            for (int i = 0; i < count; i++)
            {
                mixingMethodButtons[i].SetUnSelected();
            }
        }
    }
}