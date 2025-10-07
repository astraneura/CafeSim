public interface IOrderStepSourceInterface
{
    string GetOrderStepName();

    Ingredient GetIngredient();

    void Interact(ICustomer customer);
}
