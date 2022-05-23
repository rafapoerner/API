public class Product
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; } //Há discordâncias aqui
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }
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