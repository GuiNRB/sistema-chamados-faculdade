using SistemaChamados.Core.Entities;
using SistemaChamados.Data;

namespace SistemaChamados.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Verifica se já existem dados
        if (context.Status.Any())
        {
            return; // Banco já foi populado
        }

        // Inserir Status
        var statusList = new List<Status>
        {
            new Status { Nome = "Aberto", Descricao = "Chamado recém criado e aguardando atribuição.", Ativo = true, DataCadastro = DateTime.Now },
            new Status { Nome = "Em Andamento", Descricao = "Um técnico já está trabalhando no chamado.", Ativo = true, DataCadastro = DateTime.Now },
            new Status { Nome = "Aguardando Resposta", Descricao = "Aguardando mais informações do usuário.", Ativo = true, DataCadastro = DateTime.Now },
            new Status { Nome = "Fechado", Descricao = "O chamado foi resolvido.", Ativo = true, DataCadastro = DateTime.Now },
            new Status { Nome = "Violado", Descricao = "O prazo de resolução (SLA) do chamado foi excedido.", Ativo = true, DataCadastro = DateTime.Now }
        };
        context.Status.AddRange(statusList);

        // Inserir Prioridades
        var prioridadesList = new List<Prioridade>
        {
            new Prioridade { Nome = "Baixa", Nivel = 1, Descricao = "Resolver quando possível.", Ativo = true, DataCadastro = DateTime.Now },
            new Prioridade { Nome = "Média", Nivel = 2, Descricao = "Prioridade normal.", Ativo = true, DataCadastro = DateTime.Now },
            new Prioridade { Nome = "Alta", Nivel = 3, Descricao = "Resolver com urgência.", Ativo = true, DataCadastro = DateTime.Now }
        };
        context.Prioridades.AddRange(prioridadesList);

        // Inserir Categorias
        var categoriasList = new List<Categoria>
        {
            new Categoria { Nome = "Hardware", Descricao = "Problemas com peças físicas do computador.", Ativo = true, DataCadastro = DateTime.Now },
            new Categoria { Nome = "Software", Descricao = "Problemas com programas e sistemas.", Ativo = true, DataCadastro = DateTime.Now },
            new Categoria { Nome = "Rede", Descricao = "Problemas de conexão com a internet ou rede interna.", Ativo = true, DataCadastro = DateTime.Now },
            new Categoria { Nome = "Acesso/Login", Descricao = "Problemas de senha ou acesso a sistemas.", Ativo = true, DataCadastro = DateTime.Now }
        };
        context.Categorias.AddRange(categoriasList);

        // Salvar as alterações
        context.SaveChanges();

        // Inserir usuário administrador padrão
        // A senha é 'admin123' - hash BCrypt
        var adminUser = new Usuario
        {
            NomeCompleto = "Administrador Neuro Help",
            Email = "admin@helpdesk.com",
            SenhaHash = "$2a$11$7Wm1iN97aWdOZpK0IptKiOqE6rW1MikaR9Jv66YE.TJLDJ/Qce/BS",
            TipoUsuario = 3, // Admin
            EspecialidadeCategoriaId = null,
            Ativo = true,
            DataCadastro = DateTime.Now
        };
        context.Usuarios.Add(adminUser);

        // Salvar todas as alterações
        context.SaveChanges();
    }
}