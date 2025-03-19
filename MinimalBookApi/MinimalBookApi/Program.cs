var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book { Id = 1, Title = "The Hitchhiker's Guide to the galaxy", Author  = "Douglas Adams"},
    new Book { Id = 2, Title = "The Martian", Author = "Andy Weir"}
};

app.MapGet("/Book", () =>
{
    return books;
});

app.MapGet("/Book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("The book was no found");
    return Results.Ok(book);
});

app.MapPost("/Book", (Book book) =>
{
    books.Add(book);
    return book;
});

app.MapPut("/Book/{id}", (Book updateBook, int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("The book was no found");

    book.Title = updateBook.Title;
    book.Author = updateBook.Author;
    return Results.Ok(book);
});


app.MapDelete("/Book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("The Book was no found");
    books.Remove(book);
    return Results.Ok(book);
});

app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
