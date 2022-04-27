using System.Diagnostics;

public interface INotifier
{
    void Notify();
}
public class Notifier1 : INotifier
{
    public void Notify()
    {
        Debug.WriteLine("Debugging from Notifier 1");
    }
}
