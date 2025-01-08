namespace WEB_ASP.NET.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ServiciosContext context) 
        {
            context.Database.EnsureCreated();

            if (context.Servicios.Any())
            {
                return;
            }
        }   

    }
}
