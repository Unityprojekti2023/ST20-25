[System.Serializable]
public class Objective
{
    public string description;
    public bool isCompleted;
    public bool isVisualCompleted; // New property

    public Objective(string description)
    {
        this.description = description;
        this.isCompleted = false;
        this.isVisualCompleted = false;
    }
}
