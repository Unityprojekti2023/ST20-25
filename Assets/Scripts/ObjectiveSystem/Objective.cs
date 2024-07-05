public class Objective
{
    public string Description;
    public bool isCompleted;
    public int Points;

    public Objective(string description, int points)
    {
        this.Description = description;
        isCompleted = false;
        this.Points = points;
    }
}
