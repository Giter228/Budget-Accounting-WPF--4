using System;

namespace Diary_WPF
{
    internal class budgetInput
    {
        public string Name { get; private set; }
        public string TypeNote { get; private set; }
        public int Balance { get; private set; }
        private bool isIncome;
        public bool IsIncome
        {
            get 
            {
                return isIncome; 
            }
            private set 
            {
                if (Balance <= 0) isIncome = false;
                else isIncome = true;
            }
        }
        public DateTime Date;

    public budgetInput(string name, string typeNote, int balance, DateTime date)
            {
                Name = name;
                TypeNote = typeNote;
                Balance = balance;
                IsIncome = IsIncome;
                Date = date;
            }
    }
}
