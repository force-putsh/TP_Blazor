using System;
namespace TP_Blazor.Pages
{
    public partial class Counter
    {
        public Counter()
        {
        }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}

