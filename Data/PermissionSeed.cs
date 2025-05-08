using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelpdeskSystem.Data
{
    public static class PermissionSeed
    {
        public static async Task SeedPermissionsAsync(ApplicationDbContext context, RoleManager<Role> roleManager)
        {
            // Verifica se já existem permissões no banco
            if (await context.Permissions.AnyAsync())
                return;

            // Definir permissões do sistema
            var permissions = new List<Permission>
            {
                // Permissões de Administração
                new Permission { Code = "ADMIN_FULL_ACCESS", Name = "Acesso Total", Description = "Acesso total ao sistema", Module = "Administração" },
                new Permission { Code = "ADMIN_MANAGE_USERS", Name = "Gerenciar Usuários", Description = "Criar, editar e excluir usuários", Module = "Administração" },
                new Permission { Code = "ADMIN_MANAGE_ROLES", Name = "Gerenciar Papéis", Description = "Criar, editar e excluir papéis", Module = "Administração" },
                new Permission { Code = "ADMIN_MANAGE_PERMISSIONS", Name = "Gerenciar Permissões", Description = "Atribuir permissões a papéis e usuários", Module = "Administração" },
                new Permission { Code = "ADMIN_VIEW_AUDIT_TRAIL", Name = "Visualizar Trilha de Auditoria", Description = "Visualizar logs de auditoria", Module = "Administração" },
                
                // Permissões de Tickets
                new Permission { Code = "TICKET_CREATE", Name = "Criar Chamados", Description = "Criar novos chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_VIEW_ALL", Name = "Visualizar Todos os Chamados", Description = "Visualizar todos os chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_VIEW_OWN", Name = "Visualizar Próprios Chamados", Description = "Visualizar apenas os próprios chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_VIEW_DEPARTMENT", Name = "Visualizar Chamados do Departamento", Description = "Visualizar chamados do departamento", Module = "Chamados" },
                new Permission { Code = "TICKET_EDIT_ALL", Name = "Editar Todos os Chamados", Description = "Editar qualquer chamado", Module = "Chamados" },
                new Permission { Code = "TICKET_EDIT_OWN", Name = "Editar Próprios Chamados", Description = "Editar apenas os próprios chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_EDIT_ASSIGNED", Name = "Editar Chamados Atribuídos", Description = "Editar chamados atribuídos", Module = "Chamados" },
                new Permission { Code = "TICKET_ASSIGN", Name = "Atribuir Chamados", Description = "Atribuir chamados a técnicos", Module = "Chamados" },
                new Permission { Code = "TICKET_CLOSE", Name = "Fechar Chamados", Description = "Fechar/resolver chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_DELETE", Name = "Excluir Chamados", Description = "Excluir chamados", Module = "Chamados" },
                new Permission { Code = "TICKET_COMMENT", Name = "Comentar em Chamados", Description = "Adicionar comentários aos chamados", Module = "Chamados" },
                
                // Permissões de Relatórios
                new Permission { Code = "REPORT_VIEW", Name = "Visualizar Relatórios", Description = "Visualizar relatórios", Module = "Relatórios" },
                new Permission { Code = "REPORT_EXPORT", Name = "Exportar Relatórios", Description = "Exportar relatórios", Module = "Relatórios" },
                
                // Permissões de Configuração
                new Permission { Code = "CONFIG_MANAGE_CATEGORIES", Name = "Gerenciar Categorias", Description = "Gerenciar categorias de chamados", Module = "Configuração" },
                new Permission { Code = "CONFIG_MANAGE_DEPARTMENTS", Name = "Gerenciar Departamentos", Description = "Gerenciar departamentos", Module = "Configuração" },
                new Permission { Code = "CONFIG_MANAGE_SYSTEM_CODES", Name = "Gerenciar Códigos do Sistema", Description = "Gerenciar códigos do sistema", Module = "Configuração" },
            };

            // Adicionar permissões ao banco
            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();

            // Criar papéis padrão se não existirem
            string[] roleNames = { "Administrador", "Gerente", "Técnico", "Usuário" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role
                    {
                        Name = roleName,
                        Description = $"Papel de {roleName}"
                    });
                }
            }

            // Atribuir permissões aos papéis
            await AssignPermissionsToRoleAsync(context, "Administrador", permissions.Select(p => p.Code).ToArray());

            await AssignPermissionsToRoleAsync(context, "Gerente", new[]
            {
                "TICKET_CREATE", "TICKET_VIEW_ALL", "TICKET_EDIT_ALL", "TICKET_ASSIGN",
                "TICKET_CLOSE", "TICKET_COMMENT", "REPORT_VIEW", "REPORT_EXPORT"
            });

            await AssignPermissionsToRoleAsync(context, "Técnico", new[]
            {
                "TICKET_CREATE", "TICKET_VIEW_ALL", "TICKET_EDIT_ASSIGNED",
                "TICKET_CLOSE", "TICKET_COMMENT"
            });

            await AssignPermissionsToRoleAsync(context, "Usuário", new[]
            {
                "TICKET_CREATE", "TICKET_VIEW_OWN", "TICKET_EDIT_OWN", "TICKET_COMMENT"
            });
        }

        private static async Task AssignPermissionsToRoleAsync(ApplicationDbContext context, string roleName, string[] permissionCodes)
        {
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
                return;

            var permissions = await context.Permissions
                .Where(p => permissionCodes.Contains(p.Code))
                .ToListAsync();

            var rolePermissions = new List<RolePermission>();
            foreach (var permission in permissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    CreatedById = "system",
                    CreatedOn = DateTime.Now
                });
            }

            await context.RolePermissions.AddRangeAsync(rolePermissions);
            await context.SaveChangesAsync();
        }
    }
}