namespace aoc_2023_csharp.Day20;

public class Module
{
    public string Name { get; }
    public ModuleType Type { get; set; }
    public List<Module> Inputs { get; } = new();
    public List<Module> Destinations { get; } = new();
    public bool IsOn { get; set; }
    public Dictionary<string, Pulse> Memory { get; } = new();

    public Module(string name, ModuleType type)
    {
        Name = name;
        Type = type;
    }
}
