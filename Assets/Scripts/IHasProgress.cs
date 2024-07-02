using System;

public interface IHasProgress
{

    public event EventHandler<OnProgressChangedEvents> OnProgressChanged;
    public class OnProgressChangedEvents : EventArgs { public float progressNormalized; }

}