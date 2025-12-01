public interface ICustomer
{
    string CustomerName { get; }
    bool GenerateOrder();
    void UpdateOrderTimer();
    void CompleteOrder();
    void ResetOrderProgress();
    void OnOrderTimeout();
    void Speak();
    void CloseDialogue();

}
