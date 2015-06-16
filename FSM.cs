using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_Transcript
{
    public enum eSTATECODE { STATE_INITIAL, STATE_NUM, STATE_TIME, STATE_ALPHA, STATE_BLANK, STATE_ACCEPT, STATE_REJECT, STATE_MAX };
    public enum eEVENTCODE
    {
        EVENT_NUM, EVENT_TIME, EVENT_ALPHA, EVENT_BLANK, EVENT_NOT_NUM, EVENT_NOT_TIME, EVENT_NOT_ALPHA,
        EVENT_NOT_BLANK, EVENT_EOF, EVENT_MAX
    };
    
    //public delegate void fptrStateFunc
    public delegate bool fptrStateFunc(string s);
    public struct sTransRules
    {
        private eSTATECODE eSTATECODE1;
        private eEVENTCODE eEVENTCODE;
        private eSTATECODE eSTATECODE2;

        public sTransRules(eSTATECODE eSTATECODE1, eEVENTCODE eEVENTCODE, eSTATECODE eSTATECODE2)
        {
            // TODO: Complete member initialization
            this.eSTATECODE1 = eSTATECODE1;
            this.eEVENTCODE = eEVENTCODE;
            this.eSTATECODE2 = eSTATECODE2;
        }
    };

    class FSM
    {
        private sTransRules[] sRULE = 
        {
            new sTransRules(eSTATECODE.STATE_INITIAL, eEVENTCODE.EVENT_NUM, eSTATECODE.STATE_NUM),
            new sTransRules(eSTATECODE.STATE_INITIAL, eEVENTCODE.EVENT_NOT_NUM, eSTATECODE.STATE_REJECT),

            new sTransRules(eSTATECODE.STATE_NUM, eEVENTCODE.EVENT_TIME, eSTATECODE.STATE_TIME),
            new sTransRules(eSTATECODE.STATE_NUM, eEVENTCODE.EVENT_NOT_TIME, eSTATECODE.STATE_REJECT),

            new sTransRules(eSTATECODE.STATE_TIME, eEVENTCODE.EVENT_ALPHA, eSTATECODE.STATE_ALPHA),
            new sTransRules(eSTATECODE.STATE_TIME, eEVENTCODE.EVENT_NOT_ALPHA, eSTATECODE.STATE_REJECT),

            new sTransRules(eSTATECODE.STATE_ALPHA, eEVENTCODE.EVENT_BLANK, eSTATECODE.STATE_BLANK),
            new sTransRules(eSTATECODE.STATE_ALPHA, eEVENTCODE.EVENT_NOT_BLANK, eSTATECODE.STATE_REJECT),

            new sTransRules(eSTATECODE.STATE_BLANK, eEVENTCODE.EVENT_NUM, eSTATECODE.STATE_NUM),
            new sTransRules(eSTATECODE.STATE_BLANK, eEVENTCODE.EVENT_EOF, eSTATECODE.STATE_ACCEPT),

        };
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