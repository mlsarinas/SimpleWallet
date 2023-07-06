using Model.DAL;
public class AccountRegistration

{
    private IAccountDAL _accountDAL;
    public AccountRegistration(IAccountDAL accountDAL)
    {
        _accountDAL = accountDAL;
    }
    public void Register()
    {
        Console.WriteLine("Please enter Account UserName: ");
        var userName = Console.ReadLine();


    }

}