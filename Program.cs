using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

var app = builder.Build();
var configuration = app.Configuration;
ProductRepository.Init(configuration);


app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) =>
{
    var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
    var product = new Product{
        Code = productRequest.Code, 
        Name = productRequest.Name, 
        Description = productRequest.Description, 
        Category = category
    };
    if(productRequest.Tags != null) {
        product.Tags = new List<Tag>();
        foreach (var item in productRequest.Tags)
        {
            product.Tags.Add(new Tag { Name = item });
        }
    }
    context.Products.Add(product);
    context.SaveChanges();
    return Results.Created($"/products/{product.Id}", product.Id);
});

app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    if (product != null)
        return Results.Ok(product);
    return Results.NotFound();
});

app.MapPut("/products", (Product product) =>
{
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok(productSaved);
});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();
});

// if(app.Environment.IsStaging()) Para publicar em ambientes diferentes
app.MapGet("/configuration/database", (IConfiguration configuration) =>
{
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}");
});

app.Run();
