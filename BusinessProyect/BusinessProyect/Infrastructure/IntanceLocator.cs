namespace BusinessProyect.Infrastructure
{
    using BusinessProyect.ViewModel;

    public class IntanceLocator
    {
        public MainViewModel Main { get; set; }

        public IntanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
