using System.Text.Json.Serialization;

namespace WarcraftTalents.Talents;

public class JsonData
{
    public class Node
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("posX")]
        public int PosX { get; set; }

        [JsonPropertyName("posY")]
        public int PosY { get; set; }

        [JsonPropertyName("maxRanks")]
        public int MaxRanks { get; set; }

        [JsonPropertyName("entryNode")]
        public bool EntryNode { get; set; }

        [JsonPropertyName("next")]
        public List<int> Next { get; set; }

        [JsonPropertyName("prev")]
        public List<int> Prev { get; set; }

        [JsonPropertyName("entries")]
        public List<Entry> Entries { get; set; }

        [JsonPropertyName("freeNode")]
        public bool? FreeNode { get; set; }

        [JsonPropertyName("reqPoints")]
        public int? ReqPoints { get; set; }
    }

    public class SpecNode : Node
    {
    }

    public class ClassNode : Node
    {
    }

    public class Entry
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("definitionId")]
        public int DefinitionId { get; set; }

        [JsonPropertyName("maxRanks")]
        public int MaxRanks { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("spellId")]
        public int SpellId { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("traitTreeId")]
        public int TraitTreeId { get; set; }

        [JsonPropertyName("className")]
        public string ClassName { get; set; }

        [JsonPropertyName("classId")]
        public int ClassId { get; set; }

        [JsonPropertyName("specName")]
        public string SpecName { get; set; }

        [JsonPropertyName("specId")]
        public int SpecId { get; set; }

        [JsonPropertyName("classNodes")]
        public List<ClassNode> ClassNodes { get; set; }

        [JsonPropertyName("specNodes")]
        public List<SpecNode> SpecNodes { get; set; }

        [JsonPropertyName("fullNodeOrder")]
        public List<int> FullNodeOrder { get; set; }
    }
}