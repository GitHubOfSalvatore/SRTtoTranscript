using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_Transcript
{
    class FSM
    {
        //A method to validate numbers
        public bool bSTATE_NUM_FN(string s)
        {
            int i = 0;
            bool bResult = int.TryParse(s, out i);
            return bResult;
        }

        public bool bSTATE_BLANK_FN(string s)
        {
            bool bResult = String.Equals(s, "");
            return bResult;
        }

        public bool bSTATE_TIME_FN(string s)
        {
            bool bResult = false;
            string sPattern = @"[0-9][0-9]:[0-9][0-9]:[0-9][0-9],[0-9][0-9][0-9] --> [0-9][0-9]:[0-9][0-9]:[0-9][0-9],[0-9][0-9][0-9]";
            if (System.Text.RegularExpressions.Regex.IsMatch(s, sPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                bResult = true;
            }
            return bResult;
        }

    }
}