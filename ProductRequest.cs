
// Protege a representação de Payloads de requisições de produtos. não suja a entidade
public record ProductRequest(string Code, string Name, string Description, int CategoryId, List<string> Tags);