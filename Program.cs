using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;



List<ActionStep> previousSteps = new();

ActionStep nextStep;
ActionStep pickNextStep(){
    // Car Startup
    if (previousSteps is null or [])
    {
        return new(ActionType.accelerate, Random.Shared.NextDouble());
    }


    // placeholder logic, replace later
    var types = Enum.GetValues<ActionType>();
    var type = types[Random.Shared.Next(types.Length)];
    return new(type, type == ActionType.accelerate ? Random.Shared.NextDouble() : 0);
}

double speed = 0;
float risk = 0.0f;
float calculateRisk(ActionStep action, float risk, double speed){
    int repeatCount = 0;
    int index = previousSteps.Count - 1;
    

    if (index >= 0)
    {
        do
        {
            if (action.Type == previousSteps[index].Type)
            {
                repeatCount += 1;
            }
            index--;
            
        }
        while (index >= 0);
    }

    if (repeatCount == 0)
    {
        switch (action.Type)
        {
            case ActionType.drive:
                if (speed >= 200)
                {
                    risk += 0.2f;
                }
                else if (speed >= 120)
                {
                    risk += 0.05f;
                }
                else if (speed < 30)
                {
                    risk += 0.01f;
                }
                break;

            case ActionType.accelerate:


                if (speed >= 200)
                {
                    risk += (float)((speed / 100) * action.Percentage);
                }
                else if (speed >= 120)
                {
                    risk += 0.05f;
                }
                else if (speed < 30)
                {
                    risk += 0.01f;
                }
                break;
        }
    }
    else if (action.Type == ActionType.lane_switch)
    {
        risk += (float)(0.25 * repeatCount);
    }


    previousSteps.Add(action);
    return risk;
}

// MAIN LOGIC
while (risk < 1.0f) {
    nextStep = pickNextStep();
    Console.WriteLine(nextStep.ToString());
    Console.WriteLine("Current risk:", calculateRisk(nextStep, risk, speed));
}

