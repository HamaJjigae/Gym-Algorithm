namespace DBConnectedFinalProjectThing
{
    public partial class App : Application
    {
        public static string ConnectionString = "User Id=GYM_ADMIN;Password=1234asdf;Data Source=localhost:1521/gym_pdb;";
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
