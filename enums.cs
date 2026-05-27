using System;

enum ActionType
{
    drive,
    accelerate,
    brake,
    signal,
    lane_switch
}
struct ActionStep
{
    public ActionType Type;
    public double Percentage;

    public ActionStep(ActionType type, double percentage = 0)
    {
        Type = type;
        Percentage = percentage;
    }
}
