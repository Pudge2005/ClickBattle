namespace Game.Ui
{
    public static class NumericsShortStringConverter
    {
        public static string FloatToShortString(float floatingNum)
        {
            return floatingNum.ToString("N0");
        }

        public static string IntToShortString(int integerNum)
        {
            return integerNum.ToString();
        }
    }
}
