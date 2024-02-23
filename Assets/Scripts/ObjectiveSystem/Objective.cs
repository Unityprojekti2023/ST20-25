[System.Serializable]
public class Objective
{
    public string description;
    public bool isCompleted;
    public bool isVisualCompleted;
    public int scoreValue;

    public Objective(string description, int scoreValue)
    {
        this.description = description;
        this.isCompleted = false;
        this.isVisualCompleted = false;
        this.scoreValue = scoreValue;
    }
}
