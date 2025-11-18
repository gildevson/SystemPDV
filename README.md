🔒 Esquema Completo do Sistema de AutenticaçãoA seguir, está o esquema completo do banco de dados para o sistema de autenticação, incluindo as tabelas de Usuários, Permissões e a tabela de relacionamento UsuarioPermissao, juntamente com o INSERT inicial de permissões.1️⃣ Tabela UsuariosArmazena as informações básicas dos usuários.SQLCREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(500) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT(GETDATE())
);
Chave Primária: Id (GUID/UNIQUEIDENTIFIER).Restrição: O campo Email deve ser único.Segurança: A senha é armazenada como SenhaHash.2️⃣ Tabela PermissoesDefine as diferentes permissões (funções) disponíveis no sistema.SQLCREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200) NULL
);
💡 Inserção das Permissões IniciaisSQLINSERT INTO Permissoes (Nome, Descricao) VALUES
('ADMIN', 'Administrador do sistema'),
('USUARIO', 'Acesso básico ao sistema');
IdNomeDescricao1ADMINAdministrador do sistema2USUARIOAcesso básico ao sistema3️⃣ Tabela UsuarioPermissao (Relacionamento N:M)Esta tabela associa quais Permissoes pertencem a cada Usuario.SQLCREATE TABLE UsuarioPermissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    PermissaoId INT NOT NULL,

    CONSTRAINT FK_UsuarioPermissao_Usuario
        FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios (Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_UsuarioPermissao_Permissao
        FOREIGN KEY (PermissaoId)
        REFERENCES Permissoes (Id)
        ON DELETE CASCADE,

    CONSTRAINT UK_Usuario_Permissao UNIQUE (UsuarioId, PermissaoId)
);
Comportamento: Utiliza ON DELETE CASCADE.Restrição: Impede permissões duplicadas (UK_Usuario_Permissao).🔥 Exemplo Completo para Criação (DO ZERO)Script completo para copiar e executar:SQLDROP TABLE IF EXISTS UsuarioPermissao;
DROP TABLE IF EXISTS Usuarios;
DROP TABLE IF EXISTS Permissoes;

CREATE TABLE Usuarios (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Nome NVARCHAR(150) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    SenhaHash NVARCHAR(500) NOT NULL,
    DataCriacao DATETIME NOT NULL DEFAULT(GETDATE())
);

CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(200) NULL
);

INSERT INTO Permissoes (Nome, Descricao) VALUES
('ADMIN', 'Administrador do sistema'),
('USUARIO', 'Acesso básico ao sistema');

CREATE TABLE UsuarioPermissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId UNIQUEIDENTIFIER NOT NULL,
    PermissaoId INT NOT NULL,

    CONSTRAINT FK_UsuarioPermissao_Usuario
        FOREIGN KEY (UsuarioId)
        REFERENCES Usuarios (Id)
        ON DELETE CASCADE,

    CONSTRAINT FK_UsuarioPermissao_Permissao
        FOREIGN KEY (PermissaoId)
        REFERENCES Permissoes (Id)
        ON DELETE CASCADE,

    CONSTRAINT UK_Usuario_Permissao UNIQUE (UsuarioId, PermissaoId)
);
