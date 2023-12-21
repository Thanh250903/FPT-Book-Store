namespace ASM1670.Utility;

<<<<<<< HEAD
public static class Constraintt
{
    public const string Admin = "Admin";
    public const string User = "Customer";
=======
public static class Constraintt // neu ko bo static thi khi chay app lan dau se ko an, tru khi build neu la static thi chi can an nut chay la xong 
{
    // tao ra nhung hang so string de tien cho viec (su dung lai)
    public const string Admin = "Authenticated";
    public const string User = "UnAuthenticated";
>>>>>>> 7fb99d824d7fb4f33e04ee52783ce70f0ed8ad17
    public const string AdminRole = "Admin";
    public const string OwnerRole = "Owner";
    public const string UserRole = "User";
    public const string ssShoppingCart = "Shopping Cart Session";
}