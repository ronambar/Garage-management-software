
namespace Ex03.GarageLogic
{
    public class ValidationChecks
    {
        public static bool IsNumberStr(string i_StrToCheck)
        {
            bool isNumber = true;

            for (int i = 0; i < i_StrToCheck.Length && isNumber == true; i++)
            {
                if (char.IsNumber(i_StrToCheck[i]) == false)
                {
                    isNumber = false;
                }
            }

            return isNumber;
        }

        public static bool IsFloatNumberInRange(float i_NumberToCheck, float i_MinRangeNum, float i_MaxRangeNum)
        {
            return i_NumberToCheck >= i_MinRangeNum && i_NumberToCheck <= i_MaxRangeNum;
        }

        public static bool IsValidChoice(string i_UserChoice, int i_MinValidChoice, int i_MaxValidChoice)
        {
            return IsNumberStr(i_UserChoice) && int.Parse(i_UserChoice) >= i_MinValidChoice && int.Parse(i_UserChoice) <= i_MaxValidChoice;
        }

        public static bool IsValidStrLen(string i_StrToCheck, int i_MinStrLen, int i_MaxStrLen)
        {
            return (i_StrToCheck.Length >= i_MinStrLen) && (i_StrToCheck.Length <= i_MaxStrLen);
        }

        public static bool IsStrEmpty(string i_StrToCheck)
        {
            return i_StrToCheck.Length == 0;
        }

        public static bool IsLettersStr(string i_StrToCheck)
        {
            bool isNumber = true;

            for (int i = 0; i < i_StrToCheck.Length && isNumber == true; i++)
            {
                if (char.IsLetter(i_StrToCheck[i]) == false)
                {
                    isNumber = false;
                }
            }

            return isNumber;
        }
    }
}
