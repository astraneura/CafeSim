using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public string customerID;
    public string drinkName;
    public List<OrderStep> steps;
    public int currentStepIndex;
    public bool isCompleted;

    public Order(string customerID, DrinkRecipe recipe)
    {
        this.customerID = customerID;
        this.drinkName = recipe.drinkName;

        steps = new List<OrderStep>();

        foreach (string step in recipe.steps)
        {
            steps.Add(new OrderStep { stepName = step });
        }
        currentStepIndex = 0;
        isCompleted = false;
    }

    public bool TryStep(string attemptedStep)
    {
        if (isCompleted || currentStepIndex >= steps.Count)
            return false;

        if (steps[currentStepIndex].stepName == attemptedStep)
        {
            steps[currentStepIndex].isCompleted = true;
            currentStepIndex++;
            if (currentStepIndex >= steps.Count)
            {
                isCompleted = true;
            }
            return true;
        }

        Reset();
        return false;
    }

    public void Reset()
    {
        foreach (var step in steps)
            step.isCompleted = false;

        currentStepIndex = 0;
        Debug.Log(currentStepIndex);
        isCompleted = false;

        // Reset DrinkManager balances when order is reset
        DrinkManager.Instance.ResetDrinkValues();
    }
}
