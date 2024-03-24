namespace WarcraftTalents.Talents;

public static class BitHelpers
{
    public const int BitsPerChar = 6;

    public static readonly char[] NumberToBase64CharConversionTable = MakeBase64ConversionTable();
    public static readonly Dictionary<char, byte> Base64CharToNumberConversionTable = MakeBase64ConversionTable().Select((x, i) => new KeyValuePair<char, byte>(x, (byte)i)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    private static char[] MakeBase64ConversionTable()
    {
        List<char> base64ConversionTable = ['A'];

        for (var num = 1; num <= 25; num++)
        {
            base64ConversionTable.Add((char)(65 + num));
        }
        for (var num = 0; num <= 25; num++)
        {
            base64ConversionTable.Add((char)(97 + num));
        }
        for (var num = 0; num <= 9; num++)
        {
            base64ConversionTable.Add(num.ToString()[0]);
        }

        base64ConversionTable.Add('+');
        base64ConversionTable.Add('/');

        return base64ConversionTable.ToArray();
    }

    public static byte[] ConvertFromBase64(string exportString)
    {
        var dataValues = new List<byte>();
        for (var i = 0; i < exportString.Length; i++)
        {
            dataValues.Add(Base64CharToNumberConversionTable[exportString[i]]);
        }

        return dataValues.ToArray();
    }
}