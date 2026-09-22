var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Criar nossa lista

var aguas = new List<AguaDto>
{
    new AguaDto(1, "Santo Anjo"),
    new AguaDto(2, "Crystal"),
    new AguaDto(3, "Da Guarda")
};

app.MapGet("/", () => "API de agua está no ar");

app.MapGet("/api/aguas", () =>
{
   return Results.Ok(aguas); 
});

app.MapGet("/api/aguas/{id:int}", (int id) =>
{
   var agua = aguas.Find(aguaDaLista => aguaDaLista.id == id);

   if (agua is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(agua);
});

app.MapPost("/api/aguas", (aguaEntradaDto dados) =>
{
    int proximoId = aguas.Count + 1;
    var novaagua = new AguaDto(proximoId, dados.Titulo);
    aguas.Add(novaagua);

    return Results.Created($"/api/aguas/{novaagua.id}", novaagua);
});

app.MapPut("/api/aguas/{id:int}", (int id, aguaEntradaDto dados) =>
{
    int indice = aguas.FindIndex(aguaDaLista => aguaDaLista.id == id);

    if (indice == -1)
    {
        return Results.NotFound(new { mensagem = "Agua não encontrada." });
    }

    var atualizado = new AguaDto(id, dados.Titulo);
    aguas[indice] = atualizado;

    return Results.Ok(atualizado);
});

app.MapDelete("/api/aguas/{id:int}", (int id) =>
{
    int indice = aguas.FindIndex(aguaDaLista => aguaDaLista.id == id);

    if (indice == -1)
    {
        return Results.NotFound(new { mensagem = "Agua não encontrada." });
    }

    aguas.RemoveAt(indice);

    return Results.NoContent();
});


app.Run();

record AguaDto(int id, string Titulo);
record aguaEntradaDto(string Titulo);