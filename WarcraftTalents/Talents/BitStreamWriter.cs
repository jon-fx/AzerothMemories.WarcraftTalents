using System.Text;

namespace WarcraftTalents.Talents;

public sealed class BitStreamWriter
{
    private readonly StringBuilder _exportString = new();
    private readonly List<byte> _data = new();

    private int _totalBits = 0;
    private int _currentValue = 0;
    private int _currentReservedBits = 0;

    public BitStreamWriter()
    {
    }

    public void TryWriteValue(int bitWidth, int value)
    {
        var remainingValue = value;
        var remainingRequiredBits = bitWidth;
        var maxValue = 1 << remainingRequiredBits;
        if (remainingValue >= maxValue)
        {
            throw new NotImplementedException();
        }

        _totalBits += remainingRequiredBits;

        while (remainingRequiredBits > 0)
        {
            var spaceInCurrentValue = BitHelpers.BitsPerChar - _currentReservedBits;
            var maxStorableValue = 1 << spaceInCurrentValue;
            var remainder = remainingValue % maxStorableValue;
            remainingValue >>= spaceInCurrentValue;

            _currentValue += remainder << _currentReservedBits;

            if (spaceInCurrentValue > remainingRequiredBits)
            {
                _currentReservedBits = (_currentReservedBits + remainingRequiredBits) % BitHelpers.BitsPerChar;

                remainingRequiredBits = 0;
            }
            else
            {
                _data.Add((byte)_currentValue);
                _exportString.Append(BitHelpers.NumberToBase64CharConversionTable[_currentValue]);

                _currentValue = 0;
                _currentReservedBits = 0;

                remainingRequiredBits -= spaceInCurrentValue;
            }
        }
    }

    public void TryWriteValue(bool value)
    {
        TryWriteValue(1, value ? 1 : 0);
    }

    public string GetTalentString()
    {
        if (_currentReservedBits > 0)
        {
            _exportString.Append(BitHelpers.NumberToBase64CharConversionTable[_currentValue]);

            _currentValue = 0;
            _currentReservedBits = 0;
        }

        return _exportString.ToString();
    }
}