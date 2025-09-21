using G_Task.Common.Exceptions;
using System.Text.RegularExpressions;

namespace G_Task.Common.Helpers;

public static class StringExtensionService
{
    /// <summary>
    /// طول کد ملی حقیقی
    /// </summary>
    public const int RealNationalNumberLength = 10;


    /// <summary>
    /// بررسی صحت کدملی حقیقی
    /// </summary>
    public static bool VerifyRealNationalNumber(this string nationalNumber)
    {
        nationalNumber = nationalNumber?.Trim();

        if (string.IsNullOrWhiteSpace(nationalNumber)) return false;

        if (nationalNumber.Length != RealNationalNumberLength) return false;

        for (int i = 0; i < 10; i++)
        {
            var bad = new string((char)i, RealNationalNumberLength);

            if (bad.Equals(nationalNumber)) return false;
        }

        var chArray = nationalNumber.ToCharArray();
        var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
        var a = Convert.ToInt32(chArray[9].ToString());

        var b = num0 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;
        var c = b % 11;

        return (c < 2 && a == c || c >= 2 && 11 - c == a) ? true :
            throw new NotFoundException(string.Format(ErrorMessages.ValidationNationalCodeInvalid, nationalNumber));

    }


    public static string SafeTrim(this string str)
    {
        return (str ?? string.Empty).Trim();
    }


}