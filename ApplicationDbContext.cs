using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
         .Property(p => p.Description).HasMaxLength(500).IsRequired(false);
        builder.Entity<Product>()
         .Property(p => p.Name).HasMaxLength(120).IsRequired();
        builder.Entity<Product>()
         .Property(p => p.Code).HasMaxLength(20).IsRequired();
    }
}






//api.app.com/users?datestart={date}&dateend={date} - Por Query Params
// app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
// {
//     return dateStart + " - " + dateEnd;
// });

// api.app.com/getproductbyheader/ - Por Header
// app.MapGet("/getproductbyheader", (HttpRequest request) =>
// {

//     return request.Headers["product-code"].ToString();

// });