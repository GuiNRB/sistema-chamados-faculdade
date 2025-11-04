# Configuração do SQL Server para o Sistema de Chamados

## Opções de Configuração

### 1. SQL Server LocalDB (Recomendado para Desenvolvimento)

**Configuração atual no `appsettings.json`:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SistemaChamados;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

**Instalação do LocalDB:**
- Baixe e instale o SQL Server Express LocalDB
- Ou instale via Visual Studio Installer

### 2. SQL Server Completo (Para Produção)

**Configuração no `appsettings.Production.json`:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaChamados;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

**Para usar SQL Server completo:**
1. Instale o SQL Server Express ou Developer Edition
2. Execute o script `Scripts/CreateDatabase_SQLServer.sql` no SQL Server Management Studio
3. Ou deixe o Entity Framework criar automaticamente via migrations

### 3. SQL Server com Autenticação SQL

**Exemplo de connection string:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SistemaChamados;User Id=sa;Password=SuaSenhaAqui;TrustServerCertificate=true;"
}
```

## Comandos Entity Framework

### Criar nova migration:
```bash
dotnet ef migrations add NomeDaMigration
```

### Aplicar migrations:
```bash
dotnet ef database update
```

### Remover última migration:
```bash
dotnet ef migrations remove
```

## Dados Iniciais

O sistema automaticamente criará:
- 5 Status (Aberto, Em Andamento, Aguardando Resposta, Fechado, Violado)
- 3 Prioridades (Baixa, Média, Alta)
- 4 Categorias (Hardware, Software, Rede, Acesso/Login)
- 1 Usuário Administrador (admin@helpdesk.com / admin123)

## Troubleshooting

### Erro de conexão:
1. Verifique se o SQL Server está rodando
2. Confirme a connection string
3. Verifique as permissões do usuário

### LocalDB não encontrado:
```bash
# Verificar instâncias do LocalDB
sqllocaldb info

# Criar nova instância se necessário
sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

### Recriar banco do zero:
1. Delete o banco no SQL Server
2. Delete a pasta `Migrations/`
3. Execute: `dotnet ef migrations add InitialMigration`
4. Execute: `dotnet ef database update`