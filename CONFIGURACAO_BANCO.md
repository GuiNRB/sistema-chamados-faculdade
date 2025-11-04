# Configuração do Banco de Dados SQL Server

## Passos para configurar o sistema com SQL Server

### 1. Verificar se o banco já existe
Se você já executou o script `Scripts/CreateDatabase.sql`, o banco `SistemaChamados` já deve existir com todas as tabelas e dados iniciais.

### 2. Marcar a migração como aplicada
Como o banco já existe, você precisa marcar a migração do Entity Framework como aplicada sem executá-la:

```bash
cd /caminho/para/sistema-chamados-faculdade
dotnet ef database update --connection "Server=localhost;Database=SistemaChamados;Trusted_Connection=True;Encrypt=False;"
```

### 3. Configurar a connection string
Verifique se o arquivo `appsettings.json` tem a connection string correta:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaChamados;Trusted_Connection=True;Encrypt=False;"
  }
}
```

### 4. Executar a aplicação
```bash
# Terminal 1 - API
dotnet run --project . --urls "http://localhost:5246"

# Terminal 2 - Web App
dotnet run --project SistemaChamados.Web --urls "http://localhost:12000"
```

### 5. Testar o login
- URL: http://localhost:12000
- Usuário: admin@helpdesk.com
- Senha: senha123

## Troubleshooting

### Erro: "no such table: Status"
Isso indica que o sistema está tentando usar SQLite. Verifique:
1. A connection string não deve conter "Data Source="
2. O banco SQL Server deve estar rodando
3. A connection string deve apontar para o SQL Server

### Erro de conexão com SQL Server
1. Verifique se o SQL Server está rodando
2. Verifique se a connection string está correta
3. Teste a conexão com SQL Server Management Studio

### Migration já aplicada
Se você receber erro de que a migration já foi aplicada, use:
```bash
dotnet ef database update --connection "sua-connection-string" --verbose
```