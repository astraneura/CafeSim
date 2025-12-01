using UnityEngine;

public class DataProcessing
{
    public float moneyMade;
    public int ordersCompleted;
    public int ordersFailed;
    public float timePlayed;

    public DataProcessing(float moneyMade, int ordersCompleted, int ordersFailed, float timePlayed)
    {
        this.moneyMade = moneyMade;
        this.ordersCompleted = ordersCompleted;
        this.ordersFailed = ordersFailed;
        this.timePlayed = timePlayed;
    }
}
