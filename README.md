# Sistema de Chamados - NeuroHelp

Sistema de helpdesk desenvolvido em ASP.NET Core MVC 8.0 com funcionalidades completas migradas da versão desktop.

## 🏗️ Arquitetura

O projeto segue uma arquitetura limpa com separação de responsabilidades:

```
SistemaChamados/
├── Core/
│   └── Entities/          # Entidades do domínio
├── Application/
│   └── DTOs/              # Data Transfer Objects
├── API/
│   └── Controllers/       # Controllers da API
└── Data/                  # Contexto do Entity Framework
```

## 🚀 Tecnologias Utilizadas

- **ASP.NET Core 8** - Framework web
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados
- **BCrypt.Net** - Hash seguro de senhas
- **Swagger/OpenAPI** - Documentação da API

## 📋 Funcionalidades Implementadas

### ✅ Registro de Usuário Admin

- **Endpoint**: `POST /api/usuarios/registrar-admin`
- **Descrição**: Registra um novo usuário do tipo Administrador
- **Validações**:
  - Email único no sistema
  - Campos obrigatórios
  - Formato de email válido
  - Senha com mínimo de 6 caracteres
- **Segurança**: Senha criptografada com BCrypt

#### Exemplo de Requisição:
```json
{
  "nomeCompleto": "Administrador do Sistema",
  "email": "admin@faculdade.edu.br",
  "senha": "Admin123!"
}
```

#### Exemplo de Resposta (201 Created):
```json
{
  "id": 1,
  "nomeCompleto": "Administrador do Sistema",
  "email": "admin@faculdade.edu.br",
  "tipoUsuario": 3,
  "dataCadastro": "2025-09-16T02:45:00.000Z",
  "ativo": true
}
```

## 🗄️ Banco de Dados

### Script de Criação
Execute o script `Scripts/CreateDatabase.sql` no SQL Server para criar todas as tabelas necessárias.

### Estrutura das Tabelas

O projeto utiliza as seguintes entidades principais:

1. **Usuarios**: Informações básicas dos usuários do sistema
2. **AlunoPerfil**: Perfil específico para alunos (relacionamento 1:1 com Usuarios)
3. **ProfessorPerfil**: Perfil específico para professores (relacionamento 1:1 com Usuarios)
4. **Categorias**: Categorias para classificação dos chamados
5. **Chamados**: Chamados de suporte técnico
6. **HistoricoChamado**: Histórico de alterações nos chamados

### Tipos de Usuário:
- `1` - Aluno
- `2` - Professor  
- `3` - Administrador

### Relacionamentos:
- Usuario 1:1 AlunoPerfil (opcional)
- Usuario 1:1 ProfessorPerfil (opcional)
- Usuario 1:N Chamados (como solicitante)
- Usuario 1:N Chamados (como atribuído)
- Categoria 1:N Chamados
- Chamado 1:N HistoricoChamado

## ⚙️ Configuração

### Pré-requisitos:
- .NET 8 SDK
- SQL Server (LocalDB ou instância completa)

### String de Conexão:
Configure no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaChamados;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Executar o Projeto:
```bash
dotnet run
```

A API estará disponível em:
- HTTPS: `https://localhost:7000`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:7000/swagger`

## 🧪 Testes

Use o arquivo `test-admin-register.http` para testar os endpoints com diferentes cenários:
- Registro bem-sucedido
- Email duplicado
- Dados inválidos

## 🔒 Segurança

- **Hash de Senhas**: Utiliza BCrypt com salt automático
- **Validação de Entrada**: Data Annotations para validação
- **CORS**: Configurado para desenvolvimento
- **HTTPS**: Redirecionamento automático

## 🚀 Funcionalidades Migradas

- ✅ **Sistema de Autenticação JWT** com redirecionamento baseado no tipo de usuário
- ✅ **Dashboards Específicos** para cada tipo de usuário:
  - Usuário (TipoUsuario = 1): Dashboard do usuário
  - Técnico (TipoUsuario = 2): Dashboard do técnico  
  - Administrador (TipoUsuario = 3): Dashboard administrativo
- ✅ **Interface Web Responsiva** com design desktop migrado
- ✅ **Integração com OpenAI** para funcionalidades de IA
- ✅ **Banco de dados SQLite** para desenvolvimento

## 👤 Usuários de Teste

O sistema inclui usuários pré-configurados para teste:

- **Administrador**: admin@helpdesk.com / senha123
- **Usuário Teste**: admin.teste@helpdesk.com / senha123

## 🔐 Configuração de Ambiente

1. Copie o arquivo de exemplo:
```bash
cp .env.example .env
```

2. Configure sua chave da OpenAI no arquivo `.env`:
```env
OPENAI_API_KEY=sua-chave-openai-aqui
```

## 📝 Próximos Passos

- [ ] Implementar funcionalidades completas de chamados
- [ ] Adicionar sistema de notificações
- [ ] Implementar relatórios e dashboards avançados
- [ ] Adicionar testes unitários
- [ ] Configurar logging estruturado