# Sistema de Chamados - Versão Web Unificada

## Descrição

Esta é a versão web unificada do Sistema de Chamados, onde tanto o frontend (HTML/CSS/JavaScript) quanto a API C# ASP.NET Core são servidos pelo mesmo servidor na porta 5246.

## Mudanças Implementadas

### 1. Estrutura de Arquivos
- **Criado diretório `wwwroot/`**: Contém todos os arquivos do frontend
- **Movidos arquivos do Desktop**: Todos os arquivos HTML, CSS, JS e imagens foram movidos para `wwwroot/`
- **Renomeado arquivo principal**: `login-desktop.html` → `index.html` (página padrão)

### 2. Configuração do Servidor
- **Program.cs atualizado**: Adicionado middleware para servir arquivos estáticos
  - `app.UseDefaultFiles()`: Serve `index.html` automaticamente na raiz
  - `app.UseStaticFiles()`: Serve todos os arquivos estáticos da pasta `wwwroot/`
- **CORS removido**: Não é mais necessário pois frontend e backend estão na mesma origem

### 3. JavaScript Atualizado
- **URLs relativas**: Todas as chamadas da API agora usam URLs relativas (`/api/...`)
- **Variável API_BASE**: Alterada de `"http://localhost:5246"` para `""` (vazio)

### 4. Banco de Dados
- **SQLite**: Configurado para usar SQLite em vez de SQL Server para facilitar o desenvolvimento
- **Connection String**: `"Data Source=sistemachamados.db"`

## Como Executar

### Pré-requisitos
- .NET 8.0 SDK instalado
- Git (para clonar o repositório)

### Passos para Execução

1. **Clone o repositório** (se ainda não foi feito):
   ```bash
   git clone <url-do-repositorio>
   cd sistema-chamados-faculdade
   ```

2. **Execute o projeto**:
   ```bash
   dotnet run --project SistemaChamados.csproj
   ```

3. **Acesse a aplicação**:
   - Abra o navegador e vá para: `http://localhost:5246`
   - A página de login será carregada automaticamente

### URLs Importantes

- **Aplicação Web**: `http://localhost:5246` (página de login)
- **API Swagger**: `http://localhost:5246/swagger` (documentação da API)
- **Endpoints da API**: `http://localhost:5246/api/...`

## Estrutura de Arquivos

```
sistema-chamados-faculdade/
├── wwwroot/                          # Arquivos do frontend
│   ├── index.html                    # Página de login (principal)
│   ├── admin-dashboard-desktop.html  # Dashboard do administrador
│   ├── user-dashboard-desktop.html   # Dashboard do usuário
│   ├── tecnico-dashboard.html        # Dashboard do técnico
│   ├── script-desktop.js             # JavaScript principal
│   ├── style-desktop.css             # Estilos CSS
│   ├── img/                          # Imagens
│   └── ... (outros arquivos HTML)
├── API/                              # Controllers da API
├── Application/                      # Serviços e DTOs
├── Core/                            # Entidades do domínio
├── Data/                            # Contexto do banco de dados
├── program.cs                       # Configuração principal
├── appsettings.json                 # Configurações da aplicação
└── SistemaChamados.csproj          # Arquivo do projeto
```

## Funcionalidades

- **Login/Logout**: Sistema de autenticação com JWT
- **Gestão de Chamados**: Criar, visualizar, atualizar chamados
- **Diferentes Perfis**: Usuário, Técnico, Administrador
- **Comentários**: Sistema de comentários nos chamados
- **Categorias e Prioridades**: Organização dos chamados
- **Análise com IA**: Integração com OpenAI para análise de chamados

## Desenvolvimento

### Para fazer alterações no frontend:
1. Edite os arquivos na pasta `wwwroot/`
2. Reinicie o servidor (`Ctrl+C` e `dotnet run` novamente)
3. Atualize o navegador

### Para fazer alterações na API:
1. Edite os arquivos nos diretórios `API/`, `Application/`, etc.
2. O hot reload do ASP.NET Core aplicará as mudanças automaticamente

## Observações

- O banco de dados SQLite será criado automaticamente na primeira execução
- Os logs do servidor são salvos em `server.log` (quando executado em background)
- Arquivos temporários e de build estão configurados no `.gitignore`

## Troubleshooting

### Erro de porta em uso:
```bash
# Encontrar processo usando a porta 5246
netstat -ano | findstr :5246
# Ou no Linux/Mac:
lsof -i :5246

# Matar o processo se necessário
taskkill /PID <PID> /F  # Windows
kill -9 <PID>           # Linux/Mac
```

### Erro de banco de dados:
- Verifique se o arquivo `sistemachamados.db` foi criado na raiz do projeto
- Se houver problemas, delete o arquivo `.db` e execute novamente

### Problemas com JavaScript:
- Abra as ferramentas de desenvolvedor do navegador (F12)
- Verifique o console para erros
- Verifique a aba Network para ver se as chamadas da API estão funcionando