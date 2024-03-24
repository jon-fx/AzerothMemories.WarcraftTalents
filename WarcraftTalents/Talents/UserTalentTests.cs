namespace WarcraftTalents.Talents;

public sealed class UserTalentTests
{
    private readonly JsonData.Node? _jsonDataNode;
    private int _selectedRank;

    public UserTalentTests(int x, JsonData.Node? jsonDataNode)
    {
        _jsonDataNode = jsonDataNode;
    }

    public bool IsNull => _jsonDataNode == null;

    public int X => _jsonDataNode == null ? 0 : _jsonDataNode.PosX / 600;

    public int Y => _jsonDataNode == null ? 0 : _jsonDataNode.PosY / 600;

    public int MaxRanks => _jsonDataNode?.MaxRanks ?? 0;

    public bool IsSelected => SelectedRank > 0;

    public int SelectedRank
    {
        get => _selectedRank;
        set => _selectedRank = Math.Clamp(value, 0, MaxRanks);
    }

    public bool IsPartiallyRanked
    {
        get
        {
            if (_jsonDataNode == null)
            {
                return false;
            }

            return SelectedRank < MaxRanks;
        }
    }

    public bool IsChoiceNode
    {
        get
        {
            if (_jsonDataNode == null)
            {
                return false;
            }

            return _jsonDataNode.Type == "choice";
        }
    }
}