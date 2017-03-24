namespace PayrollApp.Service.Helper
{
    public class CommonHelper
    {
        #region Utility

        public static string FindNumber(string s)
        {
            string numeric = string.Empty;
            for (int i = s.Length - 1; i > -1; i--)
            {
                if (char.IsNumber(s[i]))
                    numeric = s[i] + numeric;
                else
                    break;
            }
            return numeric;
        }

        public static string FindAlphas(string s)
        {
            string alpha = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(s[i]))
                    alpha = alpha + s[i];
                else
                    break;
            }
            return alpha;
        }

        #endregion
    }
}
