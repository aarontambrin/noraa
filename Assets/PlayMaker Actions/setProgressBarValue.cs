using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ProgressBar")]
    [Tooltip("Set ProgressBar current value.")]
    public class setProgressBarValue : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(ColoredProgressBar))]
        [Tooltip("ColoredProgressBar component is required.")]
        public FsmOwnerDefault gameObject;

        [TitleAttribute("Value")]
        [Tooltip("The current value for ProgressBar.")]
        public FsmInt currentValue;

        [Tooltip("Check this box to preform this action every frame.")]
        public FsmBool everyFrame;

        ColoredProgressBar progressBar;

        public override void Reset()
        {
            gameObject = null;
            currentValue = 0;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            progressBar = go.GetComponent<ColoredProgressBar>();

            DoMeshChange();


            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoMeshChange();
            }
        }

        void DoMeshChange()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (progressBar == null)
            {
                Debug.LogError("No ColoredProgressBar component was found on " + go);
                return;
            }

            progressBar.SetProgress(currentValue.Value);
        }
    }
}