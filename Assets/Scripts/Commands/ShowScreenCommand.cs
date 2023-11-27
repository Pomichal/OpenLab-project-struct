public class ShowScreenCommand<ScreenT> : ICommand
{
    public void Execute()
    {
        App.screenManager.Show<ScreenT>();
    }
}
