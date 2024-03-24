using System.Collections.Specialized;

namespace WarcraftTalents.Talents;

public sealed class BitStreamReader
{
    private readonly byte[] _dataValues;

    private int _currentIndex;
    private int _currentExtractedBits;
    private int _currentRemainingValue;

    public BitStreamReader(string base64String)
    {
        _dataValues = BitHelpers.ConvertFromBase64(base64String);

        if (_dataValues.Length > 0)
        {
            _currentRemainingValue = _dataValues[0];
        }
    }

    public bool TryExtractValue(int bitWidth, out int value)
    {
        value = 0;

        if (_currentIndex > _dataValues.Length)
        {
            return false;
        }

        var bitsNeeded = bitWidth;
        var extractedBits = 0;

        while (bitsNeeded > 0)
        {
            var remainingBits = BitHelpers.BitsPerChar - _currentExtractedBits;
            var bitsToExtract = Math.Min(remainingBits, bitsNeeded);
            _currentExtractedBits += bitsToExtract;

            var maxStorableValue = 1 << bitsToExtract;
            var remainder = _currentRemainingValue % maxStorableValue;
            _currentRemainingValue >>= bitsToExtract;

            value += remainder << extractedBits;
            extractedBits += bitsToExtract;
            bitsNeeded -= bitsToExtract;

            if (bitsToExtract < remainingBits)
            {
                break;
            }

            if (bitsToExtract >= remainingBits)
            {
                _currentIndex++;
                _currentExtractedBits = 0;

                if (_currentIndex < _dataValues.Length)
                {
                    _currentRemainingValue = _dataValues[_currentIndex];
                }
            }
        }

        var bitmask = new BitVector32(value);

        return true;
    }

    public bool TryExtractBool()
    {
        TryExtractValue(1, out var result);

        return result > 0;
    }
}