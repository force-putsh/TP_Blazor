using System;
namespace TP_Blazor.Shared
{
    public partial class SurveyPrompt
    {
        public SurveyPrompt()
        {
        }

        private int currentCount = 0;
        private void IncrementCount()
        {
            currentCount++;
        }
    }
}

