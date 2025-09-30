public class Dk : Character
{
  public List<string> Species { get; set; } = [];

  public override string Display()
  {
    return $"Id: {Id}\nSpecies: {string.Join(", ", Species)}\nName: {Name}\nDescription: {Description}\n";
  }
}