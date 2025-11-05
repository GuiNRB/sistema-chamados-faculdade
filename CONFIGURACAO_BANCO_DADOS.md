# Configuração de Banco de Dados - Sistema de Chamados

## Visão Geral

O sistema foi configurado para suportar tanto **SQL Server** (produção) quanto **SQLite** (desenvolvimento/demonstração) de forma condicional e automática.

## Configurações

### Para Produção (SQL Server)

1. **Configurar appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaChamados;Trusted_Connection=True;Encrypt=False;"
  },
  "UseSqliteForDemo": false
}
```

2. **Executar o projeto:**
```bash
dotnet run --project SistemaChamados.csproj --urls "http://localhost:5246"
```

### Para Desenvolvimento/Demonstração (SQLite)

1. **Configurar appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaChamados;Trusted_Connection=True;Encrypt=False;"
  },
  "UseSqliteForDemo": true
}
```

2. **Executar o projeto:**
```bash
dotnet run --project SistemaChamados.csproj --urls "http://localhost:5246"
```

## Lógica de Seleção Automática

O sistema automaticamente escolhe o banco de dados baseado em:

1. **Flag `UseSqliteForDemo`**: Se `true`, usa SQLite
2. **Connection String vazia**: Se não houver connection string configurada, usa SQLite
3. **Padrão**: SQL Server para produção

## Dados de Seed (Teste)

O sistema inclui dados de seed automáticos para teste:

### Usuário Administrador
- **Email:** admin@helpdesk.com
- **Senha:** senha123
- **Tipo:** Administrador

### Categorias
- Hardware
- Software
- Rede

### Status
- Aberto
- Em Andamento
- Fechado

### Prioridades
- Baixa (Nível 1)
- Média (Nível 2)
- Alta (Nível 3)

## Estrutura de Arquivos

```
SistemaChamados/
├── wwwroot/                    # Arquivos estáticos do frontend
│   ├── index.html             # Página de login (renomeada de login-desktop.html)
│   ├── style-desktop.css      # Estilos CSS
│   ├── script-desktop.js      # JavaScript (URLs relativas)
│   └── img/                   # Imagens
├── program.cs                 # Configuração principal com lógica híbrida
├── appsettings.json          # Configurações (inclui UseSqliteForDemo)
└── SistemaChamados.csproj    # Dependências (SQL Server + SQLite)
```

## Funcionalidades Implementadas

### ✅ Servidor Unificado
- Frontend e API rodando na mesma porta
- Arquivos estáticos servidos via ASP.NET Core
- URLs relativas no JavaScript

### ✅ Configuração Flexível
- SQL Server para produção
- SQLite para desenvolvimento
- Seleção automática baseada em configuração

### ✅ Dados de Teste
- Usuário administrador pré-configurado
- Categorias, status e prioridades básicas
- Banco criado automaticamente na primeira execução

### ✅ Funcionalidades Testadas
- Login funcionando
- Redirecionamento correto
- Painel administrativo carregando
- Logout funcionando

## Como Executar

1. **Clone o repositório**
2. **Configure o appsettings.json** conforme necessário
3. **Execute o comando:**
   ```bash
   dotnet run --project SistemaChamados.csproj --urls "http://localhost:5246"
   ```
4. **Acesse:** http://localhost:5246
5. **Faça login com:** admin@helpdesk.com / senha123

## Migração para Produção

Para usar em produção com SQL Server:

1. Configure a connection string no `appsettings.json`
2. Defina `UseSqliteForDemo: false`
3. Execute as migrations se necessário:
   ```bash
   dotnet ef database update
   ```

## Observações Importantes

- O SQLite é usado apenas para demonstração/desenvolvimento
- Para produção, sempre use SQL Server
- Os dados de seed são criados automaticamente na primeira execução
- O arquivo SQLite (`sistemachamados_demo.db`) é criado automaticamente